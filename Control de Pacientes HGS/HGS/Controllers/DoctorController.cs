using HGS.Models;
using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class DoctorController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HGSModel.Doctor> doctors = await APIService.DoctorGetList();
            return View(doctors);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HGSModel.Doctor doctor = await APIService.DoctorGetS();
            return View(doctor);
        }
    }
}