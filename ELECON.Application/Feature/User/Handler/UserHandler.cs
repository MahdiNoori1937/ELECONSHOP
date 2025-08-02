using ELECON.Application.Extensions;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using ELECON.Application.Security;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IEmailSender;
using ELECON.Domain.Interface.IUserRepository;
using ELECON.Domain.Interface.SmsSender;

namespace ELECON.Application.Feature.User.Handler;

public class UserRegisterHandler(
    IUserRepository _userRepository,
    IUserSecurityRepository _userSecurityRepository,
    UserSendSms _userSendSms)
{
    public async Task<CheckUserRegisterStatus> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        string Input = request.RegisterUserDto.RegisterInput;
        string code = 5.ChooseRandomNumber().ToString();
        Domain.Entities.User.User user =
            await _userRepository.FindByEmailOrNumberAsync(request.RegisterUserDto.RegisterInput);
        if (user != null)
            return CheckUserRegisterStatus.InputExists;

        user = new Domain.Entities.User.User
        {
            Pasword = SecretHasher.Hash(request.RegisterUserDto.Password),
            UserStatus = "Active",
            RoleId = 1
        };
        if (EmialRegex.EmailIsValid(Input))
        {
            user.Email = Input;
        }
        else
        {
            user.PhoneNumber = Input;
            await _userSendSms.SendSms(user, code);
        }
        await _userRepository.Add(user);
    
        await _userSecurityRepository.Add(new UserSecurity
        {
            FailedLoginAttempts = 0,
            SMTPCode = code,
            UserId = user.Id,
        });
        return CheckUserRegisterStatus.Success;
    }
}