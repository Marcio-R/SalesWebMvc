using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System.Collections.Generic;

namespace SalesWebMvc.Controllers;
public class RegistroVendasController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult BuscaSimples()
    {
        return View();
    }
    public IActionResult BuscaAgrupada()
    {
        return View();
    }

}
