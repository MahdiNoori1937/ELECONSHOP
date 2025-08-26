using System.Security.Claims;
using System.ServiceModel.Channels;
using ELECON.Application.Extensions;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using ELECON.Application.Feature.User.Validators;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ELECON.Presentation.Controllers;

public class LoginController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("/Register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("/Register_p")]
    public async Task<IActionResult> Register_p(UserRegisterUserDto model)
    {
        string? error = await ValidateModel(new UserRegisterValidator(), model);
        if (error != null)
        {
            return Ok(new
            {
                status = 403,
                message = error,
                type = "error"
            });
        }

        CheckUserRegisterStatus status = await mediator.Send(new UserRegisterCommand(model));
        switch (status)
        {
            case CheckUserRegisterStatus.InputExists:
            {
                return Ok(new
                {
                    status = 403,
                    message = model.RegisterInput,
                    type = "error"
                });
            }


            case CheckUserRegisterStatus.Success:
                return Ok(new
                {
                    status = 200,
                    message = ResponseMessages.SuccessMessages.SentSMTPCodeForLogin,
                    type = "success",
                    link = Url.Action("SMTPLogin", "Login", new { input = model.RegisterInput })
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

    [HttpGet("/SMTPLogin")]
    public IActionResult SMTPLogin(string input)
    {
        return View(new UserLoginSMTPCodeDto
        {
            RegisterInput = input,
        });
    }

    [HttpPost("/SMTPLogin_p")]
    public async Task<IActionResult> SMTPLogin_P(UserLoginSMTPCodeDto Model)
    {
        string ReturnUrl = TempData["ReturnUrl"] as string ?? "";
        string? error = await ValidateModel(new UserSendSmtpValidator(), Model);
        if (error != null)
        {
            return Ok(new
            {
                status = 403,
                message = error,
                type = "error"
            });
        }

        UserSMTPLoginStatus status = await mediator.Send(new LoginUserWithSMTPDtoCommand(Model));
        switch (status)
        {
            case UserSMTPLoginStatus.UserNotFound:
            {
                return Ok(new
                {
                    status = 404,
                    message = ResponseMessages.ErrorMessages.UserNotFound,
                    type = "error"
                });
            }
            case UserSMTPLoginStatus.UserLockout:
                return Ok(new
                {
                    status = 404,
                    message = ResponseMessages.ErrorMessages.UserGotTimeOut,
                    type = "error"
                });
            case UserSMTPLoginStatus.SMTPCodeFailed:
                return Ok(new
                {
                    status = 404,
                    message = ResponseMessages.ErrorMessages.SMTPError,
                    type = "error"
                });
            case UserSMTPLoginStatus.UserGotTimeOut:
                return Ok(new
                {
                    status = 404,
                    message = ResponseMessages.ErrorMessages.UserGotTimeOut,
                    type = "error"
                });
            case UserSMTPLoginStatus.Success:
            {
                List<Claim> claims = await mediator.Send(new GetUserClaimsCommand(Model.RegisterInput));
                ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new(identity);
                AuthenticationProperties properties = new()
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(principal,properties);
                
                return Ok(new
                {
                    status = 404,
                    message = ResponseMessages.ErrorMessages.UserNotFound,
                    type = "success",
                    link=ReturnUrl ?? Url.Action("Home", "Home")
                });
            }

            default:
                return Ok(new
                {
                    status = 404,
                    message = ResponseMessages.WarningMessages.SomethingsGoesWrong,
                    type = "error"
                });
        }
    }

    [HttpGet("/PasswordLogin")]
    public IActionResult PasswordLogin()
    {
        return View();
    }

    [HttpGet("/PasswordLogin_p)")]
    public IActionResult PasswordLogin_p()
    {
        return View();
    }
}