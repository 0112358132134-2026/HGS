using HGS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class AreasucursalController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            IEnumerable<HGSModel.Areasucursal>? areasucursals = await APIService<HGSModel.Areasucursal>.GetList("Areasucursal/GetList", token._token);
            return View(areasucursals);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            HGSModel.Areasucursal? areasucursal = await APIService<HGSModel.Areasucursal?>.SpecialGet("Areasucursal/GetAB", token._token);

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
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newAreasucursal, "Areasucursal/Set", token._token);

            if (generalResult != null) 
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Areasucursal? areasucursal = await APIService<HGSModel.Areasucursal?>.SpecialGet("Areasucursal/GetAB", token._token);
            return View(areasucursal);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            HGSModel.Areasucursal? areasucursal = await APIService<HGSModel.Areasucursal>.Get(id, "Areasucursal/Get/", token._token);
            return View(areasucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "AreaId", "BranchId")] HGSModel.Areasucursal updatedAreasucursal)
        {
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedAreasucursal, "Areasucursal/Update", token._token);

            if (generalResult != null) 
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Areasucursal? areasucursal = await APIService<HGSModel.Areasucursal>.SpecialGet("Areasucursal/GetAB", token._token);
            return View(areasucursal);
        }
    }
}