﻿using HGSAPI.Models;
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
        public async Task<IEnumerable<Patient>> GetList()
        {
            IEnumerable<Patient> patients = await _context.Patients.ToListAsync();
            return patients;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(Models.Patient newPatient)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Patients.Any(c => c.Dpi == newPatient.Dpi))
                {
                    _context.Patients.Add(newPatient);
                    await _context.SaveChangesAsync();
                    generalResult.Result = true;
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
        public async Task<Patient?> Get(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(Models.Patient updatedPatient)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
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
                    generalResult.Result = true;
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