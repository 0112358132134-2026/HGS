using System;
using System.Collections.Generic;

namespace HGS.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string CollegiateNumber { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Dpi { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public int SpecialtyId { get; set; }

    public virtual ICollection<Bedpatient> Bedpatients { get; } = new List<Bedpatient>();

    public virtual Speciality Specialty { get; set; } = null!;
}
