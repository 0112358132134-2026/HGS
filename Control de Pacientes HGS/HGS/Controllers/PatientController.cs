using HGS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class PatientController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List(string tosearch, string optionSearch)
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
            
            IEnumerable<HGSModel.Patient>? patients = await APIService<HGSModel.Patient>.GetList("Patient/GetList", token._token);

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

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Dpi", "Name", "Lastname", "Birthdate", "Observations")] HGSModel.Patient newPatient)
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

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newPatient, "Patient/Set", token._token);

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
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

            HGSModel.Patient? patient = await APIService<HGSModel.Patient>.Get(id, "Patient/Get/", token._token);
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Dpi", "Name", "Lastname", "Birthdate", "Observations")] HGSModel.Patient updatedPatient)
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

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedPatient, "Patient/Update", token._token);

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
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

            HGSModel.Patient? patient = await APIService<HGSModel.Patient>.Get(id, "Patient/Get/", token._token);
            return View(patient);
        }
    }
}