using Microsoft.AspNetCore.Mvc;

namespace ELECON.Presentation.Controllers;

public class HomeController : Controller
{

    public IActionResult Home()
    {
        return View();
    }
  
 
}