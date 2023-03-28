//using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace HGSModel
{
    public class Areasucursal
    {
        public int Id { get; set; }
        
        public int AreaId { get; set; }

        public int BranchId { get; set; }

        public virtual Area Area { get; set; } = null!;

        public virtual Branch Branch { get; set; } = null!;

        public string? AreaName { get; set; }

        public string? BranchName { get; set; }

        public List<SelectListItem>? Areas { get; set; }

        public List<SelectListItem>? Branches { get; set; }
    }
}