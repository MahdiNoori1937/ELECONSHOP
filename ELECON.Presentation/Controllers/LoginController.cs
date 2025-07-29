using Microsoft.AspNetCore.Mvc;

namespace ELECON.Presentation.Controllers;

public class LoginController : Controller
{
    [HttpGet("/Login")]
    public IActionResult Login()
    {
        return View();
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