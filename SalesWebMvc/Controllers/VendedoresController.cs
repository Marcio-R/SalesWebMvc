using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers;
public class VendedoresController : Controller
{
    private readonly VendedorServico _vendedorServico;
    private readonly DepartamentoServico _departamentoServico;
    public VendedoresController(VendedorServico vendedorServico, DepartamentoServico departamentoServico)
    {
        _vendedorServico = vendedorServico;
        _departamentoServico = departamentoServico;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _vendedorServico.TodosVendedoresAsync();
        return View(list);
    }
    public async Task<IActionResult> Create()
    {
        var departamentos = await _departamentoServico.TodosDepartamentoasync();
        var vieModel = new VendedorFormViewModel { Departamentos = departamentos };
        return View(vieModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Vendedor vendedor)
    {
        if (!ModelState.IsValid)
        {
            var departamento = await _departamentoServico.TodosDepartamentoasync();
            var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento };
            await _vendedorServico.InserirAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var obj = await _vendedorServico.ObterVendedorPorIdAsync(id.Value);
        if (obj == null)
        {
            return NotFound();
        }
        return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _vendedorServico.RemoverVendedorAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var objs = await _vendedorServico.ObterVendedorPorIdAsync(id.Value);
        if (objs == null)
        {
            return NotFound();
        }
        return View(objs);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var objs =  await _vendedorServico.ObterVendedorPorIdAsync(id.Value);
        if (objs == null)
        {
            return NotFound();
        }
        List<Departamento> departamentos = await _departamentoServico.TodosDepartamentoasync();
        VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = objs, Departamentos = departamentos };
        return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Vendedor vendedor)
    {
        if (!ModelState.IsValid)
        {
            var departamento = await _departamentoServico.TodosDepartamentoasync();
            var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamento };
            return View(viewModel);
        }
        if (id != vendedor.Id)
        {
            return BadRequest();
        }
        try
        {
            await _vendedorServico.AtualizarAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException)
        {

            return NotFound();
        }
        catch (DbConcurrenceException)
        {
            return BadRequest();
        }
    }

}
