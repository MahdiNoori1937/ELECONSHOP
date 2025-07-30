using ELECON.Application.Feature.User.DTOs;
using MediatR;

namespace ELECON.Application.Feature.User.Command;

public class UserRegisterCommand: IRequest<CheckUserRegisterStatus>
{
    public LoginUserDto RegisterUserDto { get; set; }

    public UserRegisterCommand(LoginUserDto registerUserDto)
    {
        RegisterUserDto = registerUserDto;
    }
}

public class LoginUserDtoCommand
{
    
} 