using Microsoft.AspNetCore.Mvc.Rendering;

namespace HGSModel
{
    public class Areasucursal
    {
        public int Id { get; set; }
        
        public int AreaId { get; set; }

        public int BranchId { get; set; }

        public string? AreaName { get; set; }

        public string? BranchName { get; set; }

        public List<SelectListItem>? Areas { get; set; }

        public List<SelectListItem>? Branches { get; set; }

        public int BedCount { get; set; }
    }
}