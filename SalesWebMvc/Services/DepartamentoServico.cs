using Microsoft.EntityFrameworkCore;
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
    public async Task<List<Departamento>> TodosDepartamentoasync()
    {
        return await _context.Departamento.OrderBy(dp => dp.Nome).ToListAsync();
    }
}
