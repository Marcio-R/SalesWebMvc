namespace SalesWebMvc.Models.ViewModels;

public class VendedorFormViewModel
{
    public Vendedor Vendedor { get; set; }
    public ICollection<Departamento> Departamentos { get; set; }
}
