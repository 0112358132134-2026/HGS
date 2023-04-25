namespace HGSAPI.Models;

public partial class Speciality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; } = new List<Doctor>();
}