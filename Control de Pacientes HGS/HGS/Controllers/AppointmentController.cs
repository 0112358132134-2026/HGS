using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class AppointmentController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            IEnumerable<HGSModel.Bedpatient>? bedpatients = await APIService<HGSModel.Bedpatient>.GetList("Bedpatient/GetList");
            bedpatients = bedpatients?.Where(bp => bp.DoctorId == id);
            bedpatients = bedpatients?.OrderBy(s => s.State);

            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.Get(id, "Doctor/Get/");
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
                    item.Patient = await APIService<HGSModel.Patient>.Get(item.PatientId, "Patient/Get/");
                }
            }
                       
            return View(bedpatients);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.Get(id, "Bedpatient/Get/");

            if (bedpatient != null)
            {
                HGSModel.Bed? bed = await APIService<HGSModel.Bed>.Get(bedpatient.BedId, "Bed/Get/");
                HGSModel.Patient? patient = await APIService<HGSModel.Patient>.Get(bedpatient.PatientId, "Patient/Get/");
                HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.Get(bedpatient.DoctorId, "Doctor/Get/");

                bedpatient.Bed = bed;
                bedpatient.Patient = patient;
                bedpatient.Doctor = doctor;
            }

            return View(bedpatient);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Bedpatient? bedpatient = await APIService<HGSModel.Bedpatient>.Get(id, "Bedpatient/Get/");
            return View(bedpatient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Annotations", "State")] HGSModel.Bedpatient updatedBedpatient)
        {
            updatedBedpatient.Reason = "...";

            if (updatedBedpatient.State)
            {
                updatedBedpatient.EndDate = DateTime.Now;
            }

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedBedpatient, "Bedpatient/Update");

            if (generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }

            return View();
        }
    }
}