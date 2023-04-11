using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : Controller
    {
        private readonly HgsContext _context = new();

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<HGSModel.Patient>> GetList()
        {
            IEnumerable<HGSModel.Patient> patients = await
                (from p in _context.Patients
                 select new HGSModel.Patient
                 {
                     Id = p.Id,
                     Dpi = p.Dpi,
                     Name = p.Name,
                     Lastname = p.Lastname,
                     Birthdate = p.Birthdate,
                     Observations = p.Observations
                 }).ToListAsync();
           
            return patients;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(HGSModel.Patient newPatient)
        {
            HGSModel.GeneralResult generalResult = new()
            {                
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Patients.Any(c => c.Dpi == newPatient.Dpi))
                {
                    Patient patient = new()
                    {
                        Dpi = newPatient.Dpi,
                        Name = newPatient.Name,
                        Lastname = newPatient.Lastname,
                        Birthdate = newPatient.Birthdate,
                        Observations = newPatient.Observations
                    };

                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();                    
                    generalResult.Message = "Success";
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
        public async Task<HGSModel.Patient?> Get(int id)
        {
            HGSModel.Patient patient = await
                (from p in _context.Patients
                 select new HGSModel.Patient
                 {
                     Id = p.Id,
                     Dpi = p.Dpi,
                     Name = p.Name,
                     Lastname = p.Lastname,
                     Birthdate = p.Birthdate,
                     Observations = p.Observations
                 }).FirstAsync(p => p.Id == id);

            return patient;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(HGSModel.Patient updatedPatient)
        {
            HGSModel.GeneralResult generalResult = new()
            {                
                Message = "Unsuccessfully"
            };

            try
            {
                var patient = await _context.Patients.FindAsync(updatedPatient.Id);
                if (patient != null)
                {
                    patient.Name = updatedPatient.Name;
                    patient.Lastname = updatedPatient.Lastname;
                    patient.Birthdate = updatedPatient.Birthdate;
                    patient.Observations = updatedPatient.Observations;

                    _context.Patients.Update(patient);
                    await _context.SaveChangesAsync();                    
                    generalResult.Message = "Success";
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