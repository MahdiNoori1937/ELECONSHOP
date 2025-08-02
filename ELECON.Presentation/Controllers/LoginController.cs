using System.ServiceModel.Channels;
using ELECON.Application.Extensions;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using ELECON.Application.Feature.User.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELECON.Presentation.Controllers;

public class LoginController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("/Register")]
    public IActionResult Register()
    {
        return View();
    }  
    
    
    [HttpGet("/Login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("/Login-P")]
    public async Task<IActionResult> Login_p(RegisterUserDto Model)
    {
        string? error = await ValidateModel(new UserRegisterValidator(), Model);
        if (error!=null)
        {
            CheckUserRegisterStatus status = await mediator.Send(new UserRegisterCommand(Model));
            switch (status)
            {
                case CheckUserRegisterStatus.EmailNotRegistered:
                {
                    return Ok(new
                    {
                        status = 403,
                        message = ResponseMessages.ErrorMessages.EmailNotRegistered,
                        type = "error"
                    });
                }
                
                case CheckUserRegisterStatus.Success:
                    return Ok(new
                    {
                        status = 201,
                        message = ResponseMessages.SuccessMessages.SentSMTPCodeForLogin,
                        type = "success",
                    });
                default:
                    return Ok(new
                    {
                        status = 404,
                        message = ResponseMessages.WarningMessages.SomethingsGoesWrong,
                        type = "warning"
                    });
             
            }
        }
        else
        {
            return Ok(new
            {
                status = 403,
                message = error,
                type = "error"
            });
        }
         
    }
    
    [HttpGet("/PasswordLogin")]
    public IActionResult PasswordLogin()
    {
        return View();
    }
    
    
    [HttpGet("/SMTPLogin")]
    public IActionResult SMTPLogin()
    {
        return View();
    }
}