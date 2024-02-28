using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services;

public class VendedorServico
{
    private readonly SalesWebMvcContext _context;

    public VendedorServico(SalesWebMvcContext context)
    {
        _context = context;
    }
    public List<Vendedor> TodosVendedores()
    {
        return _context.Vendedor.ToList();
    }
    public void Inserir(Vendedor vendedore)
    {
        _context.Add(vendedore);
        _context.SaveChanges();
    }
    public Vendedor ObterVendedorPorId(int id)
    {
        return _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(x => x.Id == id);
    }
    public void RemoverVendedor(int id)
    {
        var obj = _context.Vendedor.Find(id);
        if (obj != null)
        {
            _context.Vendedor.Remove(obj);
            _context.SaveChanges();

        }
        else
        {
            throw new ArgumentException("Vendedor não encontrado", nameof(id));
        }
    }
    public void Atualizar(Vendedor vendedor)
    {
        if (!_context.Vendedor.Any(obj => obj.Id == vendedor.Id))
        {
            throw new NotFoundException("Id não existe");
        }
        try
        {
            _context.Update(vendedor);
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException e)
        {

            throw new DbUpdateConcurrencyException(e.Message);
        }
      
    }
}
