using ELECON.Application.Extensions;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using ELECON.Application.Security;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IUserRepository;
using MediatR;

namespace ELECON.Application.Feature.User.Handler;
public class UserLoginPasswordHandler
    : IRequestHandler<LoginUserPasswordDtoCommand, UserPasswordLoginStatus>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSecurityRepository _userSecurityRepository;

    public UserLoginPasswordHandler(IUserRepository userRepository, IUserSecurityRepository userSecurityRepository)
    {
        _userRepository = userRepository;
        _userSecurityRepository = userSecurityRepository;
    }

    public async Task<UserPasswordLoginStatus> Handle(LoginUserPasswordDtoCommand request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.User.User user =
            await _userRepository.FindByEmailOrNumberAsync(request.PasswordDto.Input);
        if (user == null)
        {
            return UserPasswordLoginStatus.UserNotFound;
        }

        UserSecurity userSecurity = await _userSecurityRepository.Get(user.Id);
        if (user.UserStatus==UserStatus.Banned.GetDisplayName())
        {
            return UserPasswordLoginStatus.UserBanned;
        }
        if (userSecurity.LockoutEnd < DateTime.Now)
        {
            return UserPasswordLoginStatus.UserLockout;
        }
        
        if (user.Pasword == null)
        {
            return UserPasswordLoginStatus.PasswordNotSet;
        }

        if (!SecretHasher.Verify(request.PasswordDto.Password, user.Pasword))
        {
            userSecurity.FailedLoginAttempts++;
            if (userSecurity.FailedLoginAttempts > 3)
            {
                userSecurity.LockoutEnd = DateTime.Now.AddMinutes(3);
                userSecurity.LastFailedLogin = DateTime.Now;
                userSecurity.IsLockedOut = true;
                await _userSecurityRepository.Update(userSecurity);
                return UserPasswordLoginStatus.UserGotTimeOut;
            }
            userSecurity.LastFailedLogin = DateTime.Now;
            await _userSecurityRepository.Update(userSecurity);
            return UserPasswordLoginStatus.PasswordError;
        }
        if (user.UserStatus==UserStatus.Inactive.GetDisplayName())
        {
            return UserPasswordLoginStatus.UserNotActive;
        }
        userSecurity.FailedLoginAttempts = 0;
        userSecurity.LockoutEnd = null;
        userSecurity.IsLockedOut = false;
        await _userSecurityRepository.Update(userSecurity);
        return UserPasswordLoginStatus.Success;
    }
}