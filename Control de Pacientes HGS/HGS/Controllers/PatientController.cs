using HGS.Models;
using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class PatientController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List(string tosearch, string optionSearch)
        {
            IEnumerable<Patient> patients = await APIService.PatientGetList();

            if (tosearch != null)
            {
                switch (optionSearch)
                {
                    case "DPI":
                        patients = patients.Where(s => s.Dpi.ToLower().Contains(tosearch.ToLower()));
                        break;
                    case "NAME":
                        patients = patients.Where(s => s.Name.ToLower().Contains(tosearch.ToLower()));
                        break;
                    case "LASTNAME":
                        patients = patients.Where(s => s.Lastname.ToLower().Contains(tosearch.ToLower()));
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
        public async Task<IActionResult> Create([Bind("Dpi", "Name", "Lastname", "Birthdate", "Observations")] Patient newPatient)
        {
            HGSModel.GeneralResult generalResult = await APIService.PatientSet(newPatient);
            @ViewData["Response"] = generalResult.Message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Patient patient = await APIService.PatientGet(id);            
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id","Dpi", "Name", "Lastname", "Birthdate", "Observations")] Patient updatedPatient)
        {
            HGSModel.GeneralResult generalResult = await APIService.PatientUpdate(updatedPatient);
            @ViewData["Response"] = generalResult.Message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Patient patient = await APIService.PatientGet(id);
            return View(patient);
        }
    }
}