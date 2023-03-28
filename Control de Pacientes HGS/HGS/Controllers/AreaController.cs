using HGS.Models;
using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class AreaController : Controller
    {        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<Area> areas = await APIService.AreaGetList();
            return View(areas);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description")] Area newArea)
        {
            HGSModel.GeneralResult generalResult = await APIService.AreaSet(newArea);
            @ViewData["Response"] = generalResult.Message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Area area = await APIService.AreaGet(id);
            return View(area);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id","Name,Description")] Area updatedArea)
        {
            HGSModel.GeneralResult generalResult = await APIService.AreaUpdate(updatedArea);
            @ViewData["Response"] = generalResult.Message;
            return View();
        }
    }
}