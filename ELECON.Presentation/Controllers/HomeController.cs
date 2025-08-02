using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELECON.Presentation.Controllers;

public class HomeController(IMediator mediator) : BaseController(mediator)
{

    public IActionResult Home()
    {
        return View();
    }
  
 
}