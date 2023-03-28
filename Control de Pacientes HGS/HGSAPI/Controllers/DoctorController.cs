using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : Controller
    {
        private readonly HgsContext _context = new();

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<HGSModel.Doctor>> GetList()
        {
            IEnumerable<HGSModel.Doctor> doctors = await
                (from D in _context.Doctors
                 join s in _context.Specialities on D.SpecialtyId equals s.Id                 
                 select new HGSModel.Doctor
                 {
                     Id = D.Id,
                     CollegiateNumber = D.CollegiateNumber,
                     User = D.User,
                     Password = D.Password,
                     Dpi = D.Dpi,
                     Name = D.Name,
                     Lastname = D.Lastname,
                     Birthdate = D.Birthdate,
                     SpecialtyName = s.Name
                 }).ToListAsync();
            return doctors;
        }

        [Route("GetS")]
        [HttpGet]
        public async Task<HGSModel.Doctor> GetS()
        {
            IEnumerable<HGSModel.Speciality> specialities = await _context.Specialities.Select(s =>
            new HGSModel.Speciality
            {
                Id = s.Id,
                Name = s.Name,
            }).ToListAsync();

            HGSModel.Doctor doctor = new()
            {
                Specialities = specialities.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),
            };
            return doctor;
        }
    }
}