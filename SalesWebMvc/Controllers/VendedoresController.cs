using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers;
public class VendedoresController : Controller
{
    private readonly VendedorServico _vendedorServico;

    public VendedoresController(VendedorServico vendedorServico)
    {
        _vendedorServico = vendedorServico;
    }

    public IActionResult Index()
    {
        var list = _vendedorServico.TodosVendedores();
        return View(list);
    }
}
