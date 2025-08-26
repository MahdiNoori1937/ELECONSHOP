using ELECON.Application.Extensions;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using ELECON.Application.Security;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IUserRepository;
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