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
    public async Task<List<Vendedor>> TodosVendedoresAsync()
    {
        return await _context.Vendedor.ToListAsync();
    }
    public async Task InserirAsync(Vendedor vendedore)
    {
        _context.Add(vendedore);
        await _context.SaveChangesAsync();
    }
    public async Task<Vendedor> ObterVendedorPorIdAsync(int id)
    {
        return await _context.Vendedor.Include(obj =>  obj.Departamento).FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task RemoverVendedorAsync(int id)
    {
        var obj = await _context.Vendedor.FindAsync(id);
        if (obj != null)
        {
            _context.Vendedor.Remove(obj);
            await _context.SaveChangesAsync();

        }
        else
        {
            throw new ArgumentException("Vendedor não encontrado", nameof(id));
        }
    }
    public async Task AtualizarAsync(Vendedor vendedor)
    {
        bool hasAny = await _context.Vendedor.AnyAsync(obj => obj.Id == vendedor.Id);
        if (!hasAny)
        {
            throw new NotFoundException("Id não existe");
        }
        try
        {
            _context.Update(vendedor);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {

            throw new DbUpdateConcurrencyException(e.Message);
        }

    }
}
