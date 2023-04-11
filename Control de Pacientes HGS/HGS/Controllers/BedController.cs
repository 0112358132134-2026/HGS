using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class BedController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HGSModel.Bed>? beds = await APIService<HGSModel.Bed>.GetList("Bed/GetList");
            return View(beds);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.SpecialGet("Bed/GetAS");
            return View(bed);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("AreaSucursalId", "Size", "Annotations")] HGSModel.Bed newBed)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newBed, "Bed/Set");

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.SpecialGet("Bed/GetAS");
            return View(bed);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.Get(id, "Bed/Get/");
            return View(bed);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "AreaSucursalId", "Size", "Annotations")] HGSModel.Bed updatedBed)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedBed, "Bed/Update");

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.SpecialGet("Bed/GetAS");
            return View(bed);
        }
    }
}