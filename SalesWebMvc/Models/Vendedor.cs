using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models;

public class Vendedor
{
    public int Id { get; set; }
    [Required]
    [StringLength(60,MinimumLength = 3, ErrorMessage = "{0} nome tem que ter no minimo 3 letras")]
    public string Nome { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Digite um e-mail valido!")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Salário Base")]
    [DisplayFormat(DataFormatString ="{0:F2}")]
    public double SalarioBase { get; set; }

    [Required]
    [Display(Name = "Data Nascimento")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DataNascimento { get; set; }
    public ICollection<RegistroVendas> Vendas { get; set; } = new List<RegistroVendas>();
    public Departamento Departamento { get; set; }
    public int DepartamentoId { get; set; }

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
