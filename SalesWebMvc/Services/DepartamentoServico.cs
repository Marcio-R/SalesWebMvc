using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services;

public class DepartamentoServico
{
    private readonly SalesWebMvcContext _context;

    public DepartamentoServico(SalesWebMvcContext context)
    {
        _context = context;
    }
    public List<Departamento> TodosDepartamento()
    {
        return _context.Departamento.OrderBy(dp => dp.Nome).ToList();
    }
}
