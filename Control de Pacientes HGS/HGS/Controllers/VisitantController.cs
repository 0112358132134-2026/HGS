using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class VisitantController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string dpi)
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

            IEnumerable<Models.Patient>? patients = await APIService<Models.Patient>.GetList("Patient/GetList", token._token);

            HGSModel.Patient? patient = (from p in patients
                                         where p.Dpi == dpi
                                         select new HGSModel.Patient
                                         {
                                             Id = p.Id,
                                             Name = p.Name,
                                             Lastname = p.Lastname
                                         }).FirstOrDefault();

            @ViewData["Response"] = "NotExist";

            if (patient != null)
            {
                IEnumerable<HGSModel.Bedpatient>? bedpatients = await APIService<HGSModel.Bedpatient>.GetList("Bedpatient/GetList", token._token);
                bedpatients = bedpatients?.Where(bp => bp.PatientId == patient.Id);

                @ViewData["Response"] = "NoDating";

                if (bedpatients != null)
                {
                    @ViewData["PatientName"] = patient.Name + " " + patient.Lastname;
                    @ViewData["CountDating"] = bedpatients.Count();
                    @ViewData["Response"] = null;
                    return View(bedpatients);
                }
            }
            return View();
        }

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
    }
}