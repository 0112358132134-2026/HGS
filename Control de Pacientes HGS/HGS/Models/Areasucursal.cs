using System;
using System.Collections.Generic;

namespace HGS.Models;

public partial class Areasucursal
{
    public int Id { get; set; }

    public int AreaId { get; set; }

    public int BranchId { get; set; }

    public virtual Area? Area { get; set; } 

    public virtual ICollection<Bed> Beds { get; } = new List<Bed>();

    public virtual Branch? Branch { get; set; } 
}
