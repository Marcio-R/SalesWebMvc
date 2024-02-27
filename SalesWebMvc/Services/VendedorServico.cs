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
}
