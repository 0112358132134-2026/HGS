using HGS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class BedpatientController : Controller
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

            IEnumerable<HGSModel.Bedpatient>? bedpatients = await APIService<HGSModel.Bedpatient>.GetList("Bedpatient/GetList", token._token);
            return View(bedpatients);
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

            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.SpecialGet("Bedpatient/GetBPD", token._token);
            return View(bedpatient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("BedId", "PatientId", "Reason", "DoctorId")] HGSModel.Bedpatient newBedpatient)
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

            newBedpatient.StartDate = DateTime.Now;

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newBedpatient, "Bedpatient/Set", token._token);

            if (generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }

            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.GetBPDSet(newBedpatient, "Bedpatient/GetBPDSet", token._token);
            return View(bedpatient);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
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

            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.Get(id, "Bedpatient/Get/", token._token);
            return View(bedpatient);
        }

        [HttpPost]
        public async Task<JsonResult?> GetBedJson(int bedId)
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
                    return null;
                }
            }

            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.Get(bedId, "Bed/Get/", token._token);
            var jsonresult = new { bed };
            return Json(jsonresult);
        }

        [HttpPost]
        public async Task<JsonResult?> GetPatientJson(int patientId)
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
                    return null;
                }
            }

            HGSModel.Patient? patient = await APIService<HGSModel.Patient>.Get(patientId, "Patient/Get/", token._token);
            var jsonresult = new { patient };
            return Json(jsonresult);
        }

        [HttpPost]
        public async Task<JsonResult?> GetDoctorJson(int doctorId)
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
                    return null;
                }
            }

            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.Get(doctorId, "Doctor/Get/", token._token);
            var jsonresult = new { doctor };
            return Json(jsonresult);
        }
    }
}