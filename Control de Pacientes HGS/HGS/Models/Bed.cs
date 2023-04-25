namespace HGS.Models;

public partial class Bed
{    
    public int Id { get; set; }

    public int AreaSucursalId { get; set; }

    public string Size { get; set; } = null!;

    public string? Annotations { get; set; }

    public bool State { get; set; }

    public virtual Areasucursal AreaSucursal { get; set; } = null!;

    public virtual ICollection<Bedpatient> Bedpatients { get; } = new List<Bedpatient>();
}