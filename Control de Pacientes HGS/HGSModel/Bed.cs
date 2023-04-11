using Microsoft.AspNetCore.Mvc.Rendering;

namespace HGSModel
{
    public class Bed
    {
        public int Id { get; set; }

        public int AreaSucursalId { get; set; }

        public string Size { get; set; } = null!;

        public string? Annotations { get; set; }

        public bool State { get; set; }
        
        public string? AreaSucursalName { get; set; }
        
        public List<SelectListItem>? AreaSucursals { get; set; }

        public List<SelectListItem>? Sizes { get; set; }
    }
}