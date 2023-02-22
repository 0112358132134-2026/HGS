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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string dpi, string name, string lastname, DateTime birthdate, string? observations)
        {
            HgsContext _hgsContext = new HgsContext();
            Patient patient = new Patient()
            {
                Dpi = dpi,
                Name = name,
                Lastname= lastname,
                Birthdate = birthdate,
                Observations = observations
            };
            _hgsContext.Patients.Add(patient);
            _hgsContext.SaveChanges();
            return View();
        }
    }
}
