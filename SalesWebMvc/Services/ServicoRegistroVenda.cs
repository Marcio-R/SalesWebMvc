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
        //var result = from obj in _context.RegistroVendas select obj;
        //if (dataminima.HasValue)
        //{
        //    result = result.Where(x => x.Data >= dataminima.Value);
        //}
        //if (datamaxima.HasValue)
        //{
        //    result = result.Where(x => x.Data <= datamaxima.Value);
        //}
        //return await result.Include(x => x.Vendedor).Include(x => x.Vendedor.Departamento).OrderByDescending(x => x.Data).ToListAsync();

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
}
