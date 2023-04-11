using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class PatientController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List(string tosearch, string optionSearch)
        {
            IEnumerable<HGSModel.Patient>? patients = await APIService<HGSModel.Patient>.GetList("Patient/GetList");

            if (tosearch != null)
            {
                switch (optionSearch)
                {
                    case "DPI":
                        patients = patients?.Where(s => s.Dpi.ToLower().Contains(tosearch.ToLower()));
                        break;
                    case "NAME":
                        patients = patients?.Where(s => s.Name.ToLower().Contains(tosearch.ToLower()));
                        break;
                    case "LASTNAME":
                        patients = patients?.Where(s => s.Lastname.ToLower().Contains(tosearch.ToLower()));
                        break;
                }
            }
            return View(patients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Dpi", "Name", "Lastname", "Birthdate", "Observations")] HGSModel.Patient newPatient)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newPatient, "Patient/Set");

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Patient? patient = await APIService<HGSModel.Patient>.Get(id, "Patient/Get/");
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Dpi", "Name", "Lastname", "Birthdate", "Observations")] HGSModel.Patient updatedPatient)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedPatient, "Patient/Update");

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            HGSModel.Patient? patient = await APIService<HGSModel.Patient>.Get(id, "Patient/Get/");
            return View(patient);
        }
    }
}