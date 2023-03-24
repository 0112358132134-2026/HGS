using System.ComponentModel.DataAnnotations;
namespace HGS.Models;

public partial class Branch
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ingrese el nombre del Municipio")]
    public string Municipality { get; set; } = null!;

    public virtual ICollection<Areasucursal> Areasucursals { get; } = new List<Areasucursal>();
}