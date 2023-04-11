using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class AreasucursalController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HGSModel.Areasucursal>? areasucursals = await APIService<HGSModel.Areasucursal>.GetList("Areasucursal/GetList");
            return View(areasucursals);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HGSModel.Areasucursal? areasucursal = await APIService<HGSModel.Areasucursal?>.SpecialGet("Areasucursal/GetAB");

            if (areasucursal != null) 
            {
                if (areasucursal.Areas != null && areasucursal.Branches != null)
                {
                    if (!areasucursal.Areas.Any() && areasucursal.Branches.Any())
                    {
                        @ViewData["Response"] = "NoAreas";
                    }
                    else if (areasucursal.Areas.Any() && !areasucursal.Branches.Any())
                    {
                        @ViewData["Response"] = "NoBranches";
                    }
                    else if (!areasucursal.Areas.Any() && !areasucursal.Branches.Any())
                    {
                        @ViewData["Response"] = "NoData";
                    }
                }

            }

            return View(areasucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("AreaId", "BranchId")] HGSModel.Areasucursal newAreasucursal)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newAreasucursal, "Areasucursal/Set");

            if (generalResult != null) 
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Areasucursal? areasucursal = await APIService<HGSModel.Areasucursal?>.SpecialGet("Areasucursal/GetAB");
            return View(areasucursal);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Areasucursal? areasucursal = await APIService<HGSModel.Areasucursal>.Get(id, "Areasucursal/Get/");
            return View(areasucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "AreaId", "BranchId")] HGSModel.Areasucursal updatedAreasucursal)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedAreasucursal, "Areasucursal/Update");

            if (generalResult != null) 
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Areasucursal? areasucursal = await APIService<HGSModel.Areasucursal>.SpecialGet("Areasucursal/GetAB");
            return View(areasucursal);
        }
    }
}