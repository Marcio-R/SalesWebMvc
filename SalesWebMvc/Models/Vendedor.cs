namespace SalesWebMvc.Models;

public class Vendedor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public double SalarioBase { get; set; }
    public DateTime DataNascimento { get; set; }
    public ICollection<RegistroVendas> Vendas { get; set; } = new List<RegistroVendas>();
    public Departamento Departamento { get; set; }

    public Vendedor()
    {
    }

    public Vendedor(int id, string nome, string email, double salarioBase, DateTime dataNascimento, Departamento departamento)
    {
        Id = id;
        Nome = nome;
        Email = email;
        SalarioBase = salarioBase;
        DataNascimento = dataNascimento;
        Departamento = departamento;
    }
    public void AdicionarVenda(RegistroVendas venda)
    {
        Vendas.Add(venda);
    }
    public void RemoverVenda(RegistroVendas venda)
    {
        Vendas.Remove(venda);
    }
    public double TotalVendasVendedor(DateTime inicial, DateTime final)
    {
        return Vendas.Where(v => v.Data >= inicial && v.Data <= final).Sum(v => v.Quantia);
    }
}
