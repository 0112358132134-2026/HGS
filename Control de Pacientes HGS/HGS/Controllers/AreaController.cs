using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class AreaController : Controller
    {        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HGSModel.Area>? areas = await APIService<HGSModel.Area>.GetList("Area/GetList");
            return View(areas);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description")] HGSModel.Area newArea)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult?>.Set(newArea, "Area/Set");

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Area? area = await APIService<HGSModel.Area>.Get(id, "Area/Get/");
            return View(area);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Name,Description")] HGSModel.Area updatedArea)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult?>.Update(updatedArea, "Area/Update");

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
        }
    }
}