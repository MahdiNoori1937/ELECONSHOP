using Microsoft.AspNetCore.Mvc;

namespace ELECON.Presentation.Controllers;

public class Home : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}