using ELECON.Application.Extensions;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IUserRepository;
using MediatR;

namespace ELECON.Application.Feature.User.Handler;

public class UserLoginSMTPHandler : IRequestHandler<LoginUserWithSMTPDtoCommand, UserSMTPLoginStatus>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSecurityRepository _userSecurityRepository;

    public UserLoginSMTPHandler(IUserRepository userRepository, IUserSecurityRepository userSecurityRepository)
    {
        _userRepository = userRepository;
        _userSecurityRepository = userSecurityRepository;
    }

    public async Task<UserSMTPLoginStatus> Handle(LoginUserWithSMTPDtoCommand request,
        CancellationToken cancellationToken)
    {
        UserSecurity userSecurity =
            await _userSecurityRepository.GetUserSecurityByInput(request.SmtpCodeDto.RegisterInput);
       
        if (userSecurity == null)
        {
            return UserSMTPLoginStatus.UserNotFound;
        }

        if (userSecurity.LockoutEnd < DateTime.Now)
        {
            return UserSMTPLoginStatus.UserLockout;
        }

        if (userSecurity.SMTPCode != request.SmtpCodeDto.SMTPCode)
        {
            userSecurity.LastFailedLogin = DateTime.Now;
            userSecurity.FailedLoginAttempts++;
            if (userSecurity.FailedLoginAttempts > 3)
            {
                userSecurity.LockoutEnd = DateTime.Now.AddMinutes(3);
                userSecurity.IsLockedOut = true;
                await _userRepository.ChangeUserStatus(userSecurity.UserId, UserStatus.Inactive.GetDisplayName());
                await _userSecurityRepository.Update(userSecurity);
                return UserSMTPLoginStatus.UserGotTimeOut;
            }

            await _userSecurityRepository.Update(userSecurity);
            return UserSMTPLoginStatus.SMTPCodeFailed;
        }
        await _userRepository.ChangeUserStatus(userSecurity.UserId, UserStatus.Active.GetDisplayName());
        userSecurity.FailedLoginAttempts = 0;
        userSecurity.LockoutEnd = null;
        userSecurity.IsLockedOut = false;
        await _userSecurityRepository.Update(userSecurity);
        return UserSMTPLoginStatus.Success;
    }
}