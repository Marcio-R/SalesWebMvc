using SalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models;

public class RegistroVendas
{
    public int Id { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Data{ get; set; }
    [DisplayFormat(DataFormatString ="{0:F2}")]
    public double Quantia { get; set; }
    public StatusVenda Status { get; set; }
    public Vendedor Vendedor { get; set; }

    public RegistroVendas()
    {
    }

    public RegistroVendas(int id, DateTime data, double quantia, StatusVenda status, Vendedor vendedor)
    {
        Id = id;
        Data = data;
        Quantia = quantia;
        Status = status;
        Vendedor = vendedor;
    }

}
