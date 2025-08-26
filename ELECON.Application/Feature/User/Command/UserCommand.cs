using System.Security.Claims;
using ELECON.Application.Feature.User.DTOs;
using MediatR;

namespace ELECON.Application.Feature.User.Command;

public class UserRegisterCommand: IRequest<CheckUserRegisterStatus>
{
    public UserRegisterUserDto UserRegisterUserDto { get; set; }

    public UserRegisterCommand(UserRegisterUserDto userRegisterUserDto)
    {
        UserRegisterUserDto = userRegisterUserDto;
    }
}

public class LoginUserWithSMTPDtoCommand:IRequest<UserSMTPLoginStatus>
{
    public UserLoginSMTPCodeDto SmtpCodeDto { get; set; }

    public LoginUserWithSMTPDtoCommand(UserLoginSMTPCodeDto smtpCodeDto)
    {
        SmtpCodeDto = smtpCodeDto;
    }
} 
public class LoginUserPasswordDtoCommand:IRequest<UserPasswordLoginStatus>
{
    public UserLoginPasswordDto PasswordDto { get; set; }

    public LoginUserPasswordDtoCommand(UserLoginPasswordDto passwordDto)
    {
        PasswordDto = passwordDto;
    }
}

public class GetUserClaimsCommand : IRequest<List<Claim>>
{
    public string Input { get; set; }

    public GetUserClaimsCommand(string input)
    {
        Input = input;
    }
}