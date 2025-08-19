using ELECON.Application.Extensions;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using ELECON.Application.Security;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IEmailSender;
using ELECON.Domain.Interface.IUserRepository;
using ELECON.Domain.Interface.SmsSender;
using MediatR;

namespace ELECON.Application.Feature.User.Handler;

public class UserRegisterHandler(
    IUserRepository _userRepository,
    IUserSecurityRepository _userSecurityRepository,
    UserSendEmail _emailSender,
    UserSendSms _userSendSms) : IRequestHandler<UserRegisterCommand, CheckUserRegisterStatus>
{
    public async Task<CheckUserRegisterStatus> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        string Input = request.UserRegisterUserDto.RegisterInput;
        string code = 5.ChooseRandomNumber().ToString();
        Domain.Entities.User.User user =
            await _userRepository.FindByEmailOrNumberAsync(request.UserRegisterUserDto.RegisterInput);
        if (user != null)
            return CheckUserRegisterStatus.InputExists;

        if (request.UserRegisterUserDto.Password != null)
            user = new Domain.Entities.User.User
            {
                UserStatus = "NotActive",
                RoleId = 1,
                Pasword = SecretHasher.Hash(request.UserRegisterUserDto.Password)
            };
        if (Input.EmailIsValid())
        {
            user.Email = Input;
            await _emailSender.Send(user, code);
        }
        else
        {
            user.PhoneNumber = Input;
            await _userSendSms.Send(user, code);
        }

        (string addUserResult, int userId) = await _userRepository.Add(user);
        if (!EnumExtensions.IsStatusReached<UserRegisterStatus>(addUserResult))
        {
            return CheckUserRegisterStatus.Failed;
        }

        (string addSecurityResult, int secId) = await _userSecurityRepository.Add(new UserSecurity
        {
            FailedLoginAttempts = 0,
            SMTPCode = code,
            UserId = userId,
        });
        if (!EnumExtensions.IsStatusReached<AddUserSecurityStatus>(addSecurityResult))
        {
            return CheckUserRegisterStatus.Failed;
        }

        return CheckUserRegisterStatus.Success;
    }
}

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
                await _userSecurityRepository.Update(userSecurity);
                return UserSMTPLoginStatus.UserGotTimeOut;
            }

            await _userSecurityRepository.Update(userSecurity);
            return UserSMTPLoginStatus.SMTPCodeFailed;
        }

        userSecurity.FailedLoginAttempts = 0;
        userSecurity.LockoutEnd = null;
        userSecurity.IsLockedOut = false;
        await _userSecurityRepository.Update(userSecurity);
        return UserSMTPLoginStatus.Success;
    }
}

public class UserLoginPasswordDto
    : IRequestHandler<LoginUserPasswordDtoCommand, UserPasswordLoginStatus>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSecurityRepository _userSecurityRepository;

    public UserLoginPasswordDto(IUserRepository userRepository, IUserSecurityRepository userSecurityRepository)
    {
        _userRepository = userRepository;
        _userSecurityRepository = userSecurityRepository;
    }

    public async Task<UserPasswordLoginStatus> Handle(LoginUserPasswordDtoCommand request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.User.User user =
            await _userRepository.FindByEmailOrNumberAsync(request.PasswordDto.RegisterInput);
        if (user == null)
        {
            return UserPasswordLoginStatus.UserNotFound;
        }

        UserSecurity userSecurity = await _userSecurityRepository.Get(user.Id);
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
        userSecurity.FailedLoginAttempts = 0;
        userSecurity.LockoutEnd = null;
        userSecurity.IsLockedOut = false;
        await _userSecurityRepository.Update(userSecurity);
        return UserPasswordLoginStatus.Success;
    }
}