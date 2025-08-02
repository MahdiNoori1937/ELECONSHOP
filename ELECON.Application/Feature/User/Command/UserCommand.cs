using ELECON.Application.Feature.User.DTOs;
using MediatR;

namespace ELECON.Application.Feature.User.Command;

public class UserRegisterCommand: IRequest<CheckUserRegisterStatus>
{
    public RegisterUserDto RegisterUserDto { get; set; }

    public UserRegisterCommand(RegisterUserDto registerUserDto)
    {
        RegisterUserDto = registerUserDto;
    }
}

public class LoginUserDtoCommand
{
    
} 