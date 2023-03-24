using System.ComponentModel.DataAnnotations;
namespace HGS.Models;

public partial class Speciality
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ingrese el nombre de la especialidad")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; } = new List<Doctor>();
}