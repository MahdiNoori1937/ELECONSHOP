using ELECON.Application.Extensions;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using ELECON.Domain.Entities.User;
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
        Domain.Entities.User.User user =
            await _userRepository.FindByEmailOrNumberAsync(request.RegisterUserDto.RegisterInput);
        string code = 5.ChooseRandomNumber().ToString();
        if (user == null)
        {
            if (request.RegisterUserDto.RegisterInput.StartsWith("09"))
            {
                user = new Domain.Entities.User.User
                {
                    PhoneNumber = request.RegisterUserDto.RegisterInput,
                    UserStatus = "ACTIVE",
                    RoleId = 1,
                };
                await _userRepository.Add(user);
                await _userSecurityRepository.Add(new UserSecurity
                {
                    SMTPCode = code,
                    UserId = user.Id,
                });
                await _userSendSms.SendSms(user, code);
                return CheckUserRegisterStatus.Success;
            }
            return CheckUserRegisterStatus.EmailNotRegistered;
        }
        return CheckUserRegisterStatus.Success;
    }
}
