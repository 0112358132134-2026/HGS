using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class BedpatientController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HGSModel.Bedpatient>? bedpatients = await APIService<HGSModel.Bedpatient>.GetList("Bedpatient/GetList");
            return View(bedpatients);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.SpecialGet("Bedpatient/GetBPD");
            return View(bedpatient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("BedId", "PatientId", "Reason", "DoctorId")] HGSModel.Bedpatient newBedpatient)
        {
            newBedpatient.StartDate = DateTime.Now;

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newBedpatient, "Bedpatient/Set");

            if (generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }

            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.GetBPDSet(newBedpatient, "Bedpatient/GetBPDSet");
            return View(bedpatient);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.Get(id, "Bedpatient/Get/");
            return View(bedpatient);
        }

        [HttpPost]
        public async Task<JsonResult> GetBedJson(int bedId)
        {
            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.Get(bedId, "Bed/Get/");
            var jsonresult = new { bed };
            return Json(jsonresult);
        }

        [HttpPost]
        public async Task<JsonResult> GetPatientJson(int patientId)
        {
            HGSModel.Patient? patient = await APIService<HGSModel.Patient>.Get(patientId, "Patient/Get/");
            var jsonresult = new { patient };
            return Json(jsonresult);
        }

        [HttpPost]
        public async Task<JsonResult> GetDoctorJson(int doctorId)
        {
            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.Get(doctorId, "Doctor/Get/");
            var jsonresult = new { doctor };
            return Json(jsonresult);
        }
    }
}