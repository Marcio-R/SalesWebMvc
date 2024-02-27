using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

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
    public void Inserir(Vendedor vendedores)
    {
        _context.Add(vendedores);
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
}
