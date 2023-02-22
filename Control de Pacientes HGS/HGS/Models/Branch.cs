using System;
using System.Collections.Generic;

namespace HGS.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string Municipality { get; set; } = null!;

    public virtual ICollection<Areasucursal> Areasucursals { get; } = new List<Areasucursal>();
}
