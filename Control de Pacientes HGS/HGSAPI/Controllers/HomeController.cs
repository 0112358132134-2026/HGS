using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly HgsContext _context = new();

        [Route("DoctorExists")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> DoctorExists(HGSModel.Doctor doctor)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Message = "notExist"
            };

            HGSModel.Doctor? _doctor = await
                (from D in _context.Doctors
                 where D.User == doctor.User
                 select new HGSModel.Doctor
                 {
                     Id = D.Id,
                     User = D.User,
                     Password = D.Password
                 }).FirstOrDefaultAsync();

            if (_doctor == null)
            {                
                return generalResult;
            }

            if (_doctor.Password.Equals(doctor.Password))
            {
                generalResult.Message = "Correct";
                generalResult.Id = _doctor.Id;
                return generalResult;
            }

            generalResult.Message = "IncorrectDoctor";
            return generalResult;
        }
    }
}