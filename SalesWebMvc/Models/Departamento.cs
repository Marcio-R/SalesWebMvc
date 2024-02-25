namespace SalesWebMvc.Models;

public class Departamento
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

    public Departamento()
    {
    }

    public Departamento(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
    public void AdicionarVendedor(Vendedor vendedor)
    {
        Vendedores.Add(vendedor);
    }
    public double TotalVendasDepartamento(DateTime inicial, DateTime final)
    {
        return Vendedores.Sum(vendedor => vendedor.TotalVendasVendedor(inicial, final));
    }
}
