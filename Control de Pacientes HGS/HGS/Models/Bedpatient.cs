using System;
using System.Collections.Generic;

namespace HGS.Models;

public partial class Bedpatient
{
    public int Id { get; set; }

    public int BedId { get; set; }

    public int PatientId { get; set; }

    public string Reason { get; set; } = null!;

    public bool State { get; set; }

    public int DoctorId { get; set; }

    public string? Annotations { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Bed Bed { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
