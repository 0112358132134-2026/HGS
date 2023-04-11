using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class SpecialityController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HGSModel.Speciality>? specialities = await APIService<HGSModel.Speciality>.GetList("Speciality/GetList");
            return View(specialities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] HGSModel.Speciality newSpeciality)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newSpeciality, "Speciality/Set");

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Speciality? speciality = await APIService<HGSModel.Speciality>.Get(id, "Speciality/Get/");
            return View(speciality);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Name")] HGSModel.Speciality updatedSpeciality)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedSpeciality, "Speciality/Update");

            if (generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }

            return View();
        }
    }
}