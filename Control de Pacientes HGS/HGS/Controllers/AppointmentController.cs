using HGS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class AppointmentController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            //string? userName = User.Claims.FirstOrDefault(s => s.Type == "username")?.Value;

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
            bedpatients = bedpatients?.Where(bp => bp.DoctorId == id);
            bedpatients = bedpatients?.OrderBy(s => s.State);

            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.Get(id, "Doctor/Get/", token._token);
            if (doctor != null)
            {
                @ViewData["DoctorName"] = doctor.Name;
                @ViewData["DoctorLastName"] = doctor.Lastname;
                @ViewData["DoctorCN"] = doctor.CollegiateNumber;
            }

            if (bedpatients != null)
            {
                foreach (var item in bedpatients)
                {                    
                    item.Patient = await APIService<HGSModel.Patient>.Get(item.PatientId, "Patient/Get/", token._token);
                }
            }
                       
            return View(bedpatients);
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

            if (bedpatient != null)
            {
                HGSModel.Bed? bed = await APIService<HGSModel.Bed>.Get(bedpatient.BedId, "Bed/Get/", token._token);
                HGSModel.Patient? patient = await APIService<HGSModel.Patient>.Get(bedpatient.PatientId, "Patient/Get/", token._token);
                HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.Get(bedpatient.DoctorId, "Doctor/Get/", token._token);

                bedpatient.Bed = bed;
                bedpatient.Patient = patient;
                bedpatient.Doctor = doctor;
            }

            return View(bedpatient);
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

            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.Get(id, "Bedpatient/Get/", token._token);
            return View(bedpatient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Annotations", "State")] HGSModel.Bedpatient updatedBedpatient)
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

            updatedBedpatient.Reason = "...";

            if (updatedBedpatient.State)
            {
                updatedBedpatient.EndDate = DateTime.Now;
            }

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedBedpatient, "Bedpatient/Update", token._token);

            if (generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }

            return View();
        }
    }
}