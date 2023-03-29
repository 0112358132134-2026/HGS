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

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(HGSModel.Doctor newDoctor)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                // Verificar si el User, CollegiateNumber ya existe
                if (!_context.Doctors.Any(c => c.CollegiateNumber == newDoctor.CollegiateNumber || c.User == newDoctor.User))
                {
                    //verificar si el Dpi ya existe en Pacientes
                    generalResult.Message = "IsPatient";
                    if (!_context.Patients.Any(c => c.Dpi == newDoctor.Dpi))
                    {
                        Doctor doctor = new()
                        {
                            CollegiateNumber = newDoctor.CollegiateNumber,
                            User = newDoctor.User,
                            Password = newDoctor.Password,
                            Dpi = newDoctor.Dpi,
                            Name = newDoctor.Name,
                            Lastname = newDoctor.Lastname,
                            Birthdate = newDoctor.Birthdate,
                            SpecialtyId = newDoctor.SpecialtyId
                        };

                        _context.Doctors.Add(doctor);
                        await _context.SaveChangesAsync();
                        generalResult.Result = true;
                        generalResult.Message = "Success";
                    }
                }
            }
            catch (Exception)
            {
                generalResult.Message = "Error";
            }
            return generalResult;
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<HGSModel.Doctor> Get(int id)
        {
            IEnumerable<HGSModel.Speciality> specialities = await _context.Specialities.Select(s =>
            new HGSModel.Speciality
            {
                Id = s.Id,
                Name = s.Name,
            }).ToListAsync();

            HGSModel.Doctor doctor = await
                (from D in _context.Doctors
                 join s in _context.Specialities on D.SpecialtyId equals s.Id
                 where D.Id == id
                 select new HGSModel.Doctor
                 {
                     CollegiateNumber = D.CollegiateNumber,
                     User = D.User,
                     Password = D.Password,
                     ConfirmedPassword = D.Password,
                     Dpi = D.Dpi,
                     Name = D.Name,
                     Lastname = D.Lastname,
                     Birthdate = D.Birthdate,
                     SpecialtyName = s.Name,
                     SpecialtyId = D.SpecialtyId
                 }).FirstAsync();

            doctor.Specialities = specialities.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name,
                Selected = (s.Id == doctor.SpecialtyId)
            }).ToList();

            return doctor;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(HGSModel.Doctor updatedDoctor)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                // Verificar si el User ya existe
                if (!_context.Doctors.Any(c => c.User == updatedDoctor.User && c.Id != updatedDoctor.Id))
                {
                    var doctor = await _context.Doctors.FindAsync(updatedDoctor.Id);
                    if (doctor != null)
                    {
                        doctor.User = updatedDoctor.User;
                        doctor.Password = updatedDoctor.Password;
                        doctor.Name = updatedDoctor.Name;
                        doctor.Lastname = updatedDoctor.Lastname;
                        doctor.Birthdate = updatedDoctor.Birthdate;
                        doctor.SpecialtyId = updatedDoctor.SpecialtyId;

                        _context.Doctors.Update(doctor);
                        await _context.SaveChangesAsync();
                        generalResult.Result = true;
                        generalResult.Message = "Success";
                    }
                }
            }
            catch (Exception)
            {
                generalResult.Message = "Error";
            }
            return generalResult;
        }
    }
}