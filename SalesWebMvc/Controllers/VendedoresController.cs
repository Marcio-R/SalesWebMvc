using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers;
public class VendedoresController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
