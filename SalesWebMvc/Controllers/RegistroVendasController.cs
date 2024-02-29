using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System.Collections.Generic;

namespace SalesWebMvc.Controllers;
public class RegistroVendasController : Controller
{
    private readonly ServicoRegistroVenda _registroVendaService;

    public RegistroVendasController(ServicoRegistroVenda registroVendaService)
    {
        _registroVendaService = registroVendaService;
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task< IActionResult> BuscaSimples(DateTime? minDate, DateTime ?maxDate)
    {
        if(!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year,1,1);
        }
        if (!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }
        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
        var result = await _registroVendaService.EncontrarPorDataAsync(minDate, maxDate);
        return View(result);
    }
    public IActionResult BuscaAgrupada()
    {
        return View();
    }

}
