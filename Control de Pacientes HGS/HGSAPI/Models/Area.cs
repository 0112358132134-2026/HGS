using System;
using System.Collections.Generic;

namespace HGSAPI.Models;

public partial class Area
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Areasucursal> Areasucursals { get; } = new List<Areasucursal>();
}
