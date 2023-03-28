using HGS.Models;
using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class AreasucursalController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HGSModel.Areasucursal> areasucursals = await APIService.AreasucursalGetList();
            return View(areasucursals);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HGSModel.Areasucursal areasucursal = await APIService.AreasucursalGetAB();

            if (areasucursal.Areas!= null && areasucursal.Branches != null)
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
            return View(areasucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("AreaId","BranchId")] Areasucursal newAreasucursal)
        {
            HGSModel.GeneralResult generalResult = await APIService.AreasucursalSet(newAreasucursal);
            @ViewData["Response"] = generalResult.Message;

            HGSModel.Areasucursal areasucursal = await APIService.AreasucursalGetAB();            
            return View(areasucursal);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Areasucursal areasucursal = await APIService.AreasucursalGet(id);            
            return View(areasucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id","AreaId", "BranchId")] Areasucursal updatedAreasucursal)
        {
            HGSModel.GeneralResult generalResult = await APIService.AreasucursalUpdate(updatedAreasucursal);
            @ViewData["Response"] = generalResult.Message;
            
            HGSModel.Areasucursal areasucursal = await APIService.AreasucursalGetAB();
            return View(areasucursal);
        }
    }
}