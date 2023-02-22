using System;
using System.Collections.Generic;

namespace HGS.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string Dpi { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public string? Observations { get; set; }

    public virtual ICollection<Bedpatient> Bedpatients { get; } = new List<Bedpatient>();
}
