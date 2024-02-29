using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services;

public class ServicoRegistroVenda
{
    private readonly SalesWebMvcContext _context;

    public ServicoRegistroVenda(SalesWebMvcContext context)
    {
        _context = context;
    }
    public async Task<List<RegistroVendas>> EncontrarPorDataAsync(DateTime? dataminima, DateTime? datamaxima)
    {
       
        var result = _context.RegistroVendas.AsQueryable();

        if (dataminima.HasValue)
            result = result.Where(x => x.Data >= dataminima.Value);

        if (datamaxima.HasValue)
            result = result.Where(x => x.Data <= datamaxima.Value);

        return await result
            .Include(x => x.Vendedor)
            .Include(x => x.Vendedor.Departamento)
            .OrderByDescending(x => x.Data)
            .ToListAsync();
    }
    public async Task<List<IGrouping<Departamento,RegistroVendas>>> EncontrarPorDataAgrupadaAsync(DateTime? dataminima, DateTime? datamaxima)
    {

        var result = _context.RegistroVendas.AsQueryable();

        if (dataminima.HasValue)
            result = result.Where(x => x.Data >= dataminima.Value);

        if (datamaxima.HasValue)
            result = result.Where(x => x.Data <= datamaxima.Value);

        return await result
            .Include(x => x.Vendedor)
            .Include(x => x.Vendedor.Departamento)
            .OrderByDescending(x => x.Data)
            .GroupBy(x => x.Vendedor.Departamento)
            .ToListAsync();
    }
}
