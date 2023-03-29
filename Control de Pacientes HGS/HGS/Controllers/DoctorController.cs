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

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CollegiateNumber","User","Password","ConfirmedPassword","Dpi","Name","Lastname","Birthdate","SpecialtyId")] HGSModel.Doctor newDoctor) 
        {            
            if (!newDoctor.Password.Equals(newDoctor.ConfirmedPassword))
            {
                @ViewData["Response"] = "InconsistentPassword";                                
            }
            else
            {
                // Creamos el User del Doctor                
                newDoctor.User = GenerateUserName(newDoctor.Name, newDoctor.Lastname);
                HGSModel.GeneralResult generalResult = await APIService.DoctorSet(newDoctor);
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Doctor doctor = await APIService.DoctorGetS();
            return View(doctor);
        }

        public string GenerateUserName(string name, string lastname) 
        {
            string _name = char.ToUpper(name[0]) + name.Substring(1);
            string _lastname = char.ToUpper(lastname[0]) + lastname.Substring(1);
            return _name + "_" + _lastname;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Doctor doctor = await APIService.DoctorGet(id);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id","CollegiateNumber", "User", "Password", "ConfirmedPassword", "Dpi", "Name", "Lastname", "Birthdate", "SpecialtyId")] HGSModel.Doctor updatedDoctor)
        {
            if (!updatedDoctor.Password.Equals(updatedDoctor.ConfirmedPassword))
            {
                @ViewData["Response"] = "InconsistentPassword";
            }
            else
            {
                // Creamos el User del Doctor                
                updatedDoctor.User = GenerateUserName(updatedDoctor.Name, updatedDoctor.Lastname);
                HGSModel.GeneralResult generalResult = await APIService.DoctorUpdate(updatedDoctor);
                @ViewData["Response"] = generalResult.Message;
            }

            HGSModel.Doctor doctor = await APIService.DoctorGetS();
            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            HGSModel.Doctor doctor = await APIService.DoctorGet(id);
            return View(doctor);
        }


    }
}