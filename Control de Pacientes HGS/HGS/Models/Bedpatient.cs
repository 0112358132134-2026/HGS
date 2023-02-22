using System;
using System.Collections.Generic;

namespace HGS.Models;

public partial class Bedpatient
{
    public int Id { get; set; }

    public int BedId { get; set; }

    public string PatiendDpi { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public bool State { get; set; }

    public string DoctorCollegiateNumber { get; set; } = null!;

    public string? Annotations { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Bed Bed { get; set; } = null!;

    public virtual Doctor DoctorCollegiateNumberNavigation { get; set; } = null!;

    public virtual Patient PatiendDpiNavigation { get; set; } = null!;
}
