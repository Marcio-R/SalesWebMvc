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

    public IActionResult Index()
    {
        var list = _vendedorServico.TodosVendedores();
        return View(list);
    }
    public IActionResult Create()
    {
        var departamentos = _departamentoServico.TodosDepartamento();
        var vieModel = new VendedorFormViewModel { Departamentos = departamentos };
        return View(vieModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Vendedor vendedor)
    {
        _vendedorServico.Inserir(vendedor);
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete(int ? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var obj = _vendedorServico.ObterVendedorPorId(id.Value);
        if(obj == null)
        {
            return NotFound();
        }
        return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _vendedorServico.RemoverVendedor(id);
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Details(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var objs = _vendedorServico.ObterVendedorPorId(id.Value);
        if (objs == null)
        {
            return NotFound();
        }
        return View(objs);
    }
    public IActionResult Edit(int? id) 
    {
        if(id == null)
        {
            return NotFound();
        }
        var objs = _vendedorServico.ObterVendedorPorId(id.Value);
        if(objs == null)
        {
            return NotFound();
        }
        List<Departamento> departamentos = _departamentoServico.TodosDepartamento();
        VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = objs, Departamentos = departamentos};
        return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Vendedor vendedor)
    {
        if(id != vendedor.Id)
        {
            return BadRequest();
        }
        try
        {
            _vendedorServico.Atualizar(vendedor);
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
