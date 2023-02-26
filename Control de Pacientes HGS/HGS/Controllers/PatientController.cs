using HGS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List(string tosearch, string optionSearch)
        {
            HgsContext _HGSContext = new HgsContext();
            IEnumerable<HGSModel.Patient> patients = (from c in _HGSContext.Patients
                                                      select new HGSModel.Patient
                                                      {
                                                          Id = c.Id,
                                                          Dpi = c.Dpi,
                                                          Name = c.Name,
                                                          Lastname = c.Lastname,
                                                          Birthdate= c.Birthdate,
                                                          Observations= c.Observations
                                      
                                                      }).ToList();            

            if (!String.IsNullOrEmpty(tosearch))
            {
                switch (optionSearch)
                {
                    case "dpi":
                        patients = patients.Where(s => s.Dpi!.ToLower().Contains(tosearch.ToLower()));
                        break;
                    case "name":
                        patients = patients.Where(s => s.Name!.ToLower().Contains(tosearch.ToLower()));
                        break;
                    case "lastname":
                        patients = patients.Where(s => s.Lastname!.ToLower().Contains(tosearch.ToLower()));
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
        public IActionResult Create(string dpi, string name, string lastname, DateTime birthdate, string? observations)
        {
            HgsContext _hgsContext = new();

            IEnumerable<HGSModel.Patient> _patients = (from c in _hgsContext.Patients
                                                       where c.Dpi == dpi
                                                       select new HGSModel.Patient
                                                       {                                                          
                                                          Dpi = c.Dpi
                                                       }).ToList();

            if (!_patients.Any())
            {
                Patient patient = new()
                {
                    Dpi = dpi,
                    Name = name,
                    Lastname = lastname,
                    Birthdate = birthdate,
                    Observations = observations
                };
                _hgsContext.Patients.Add(patient);
                _hgsContext.SaveChanges();
                @ViewData["Resultado"] = "¡Paciente agregado exitosamente!";
            }
            else 
            {
                @ViewData["Resultado"] = "El paciente ya ha sido ingresado...";
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HgsContext _hgsContext = new();
            Patient patient =  _hgsContext.Patients.FirstOrDefault(c => c.Id == id);
            HGSModel.Patient patientResult = new()
            {
                Dpi = patient.Dpi,
                Name = patient.Name,
                Lastname = patient.Lastname,
                Birthdate = patient.Birthdate,
                Observations = patient.Observations
            };
            return View(patientResult);
        }

        [HttpPost]
        public IActionResult Edit(int id, string dpi, string name, string lastname, DateTime birthdate, string? observations)
        {
            HgsContext _hgsContext = new();
            Patient patient = _hgsContext.Patients.FirstOrDefault(s => s.Id == id);
            patient.Dpi = dpi;
            patient.Name = name;
            patient.Lastname = lastname;
            patient.Birthdate = birthdate;
            patient.Observations = observations;
            _hgsContext.Patients.Update(patient);
            _hgsContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            HgsContext _hgsContext = new();
            Patient patient = _hgsContext.Patients.FirstOrDefault(c => c.Id == id);
            HGSModel.Patient patientResult = new()
            {
                Dpi = patient.Dpi,
                Name = patient.Name,
                Lastname = patient.Lastname,
                Birthdate = patient.Birthdate,
                Observations = patient.Observations
            };
            return View(patientResult);
        }
    }
}