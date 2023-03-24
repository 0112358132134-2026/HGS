using HGS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGS.Controllers
{
    public class PatientController : Controller
    {
        private readonly HgsContext _context;

        public PatientController() 
        {
            _context = new HgsContext();
        }

        [HttpGet]
        public async Task<IActionResult> List(string tosearch, string optionSearch)
        {
            IEnumerable<Patient> patients = await Functions.APIService.PatientGetList();

            if (tosearch != "")
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
        public IActionResult Create(string dpi, string name, string lastname, DateTime birthdate, string? observations)
        {
            if (!_context.Patients.Any(c => c.Dpi == dpi))
            {
                Patient patient = new()
                {
                    Dpi = dpi,
                    Name = name,
                    Lastname = lastname,
                    Birthdate = birthdate,
                    Observations = observations,                    
                };
                _context.Patients.Add(patient);
                _context.SaveChanges();
                @ViewData["Result"] = "OK";
            }
            else
            {
                @ViewData["Result"] = "Error";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _context.Patients.FindAsync(id);         
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string dpi, string name, string lastname, DateTime birthdate, string? observations)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null) 
            {
                patient.Dpi = dpi;
                patient.Name = name;
                patient.Lastname = lastname;
                patient.Birthdate = birthdate;
                patient.Observations = observations;
                _context.Patients.Update(patient);
                _context.SaveChanges();
                @ViewData["Result"] = "OK";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return View(patient);
        }        
    }
}