using HGS.Models;
using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class SpecialityController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<Speciality> specialities = await APIService.SpecialityGetList();
            return View(specialities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Speciality newSpeciality)
        {
            HGSModel.GeneralResult generalResult = await APIService.SpecialitySet(newSpeciality);
            @ViewData["Response"] = generalResult.Message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Speciality speciality = await APIService.SpecialityGet(id);
            return View(speciality);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id","Name")] Speciality updatedSpeciality)
        {
            HGSModel.GeneralResult generalResult = await APIService.SpecialityUpdate(updatedSpeciality);
            @ViewData["Response"] = generalResult.Message;
            return View();
        }
    }
}