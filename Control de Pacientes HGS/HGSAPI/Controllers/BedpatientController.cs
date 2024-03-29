﻿using HGSAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BedpatientController : Controller
    {
        private readonly HgsContext _context = new();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<HGSModel.Bedpatient>> GetList()
        {
            IEnumerable<HGSModel.Bedpatient> bedpatients = await
                (from BP in _context.Bedpatients
                 join p in _context.Patients on BP.PatientId equals p.Id
                 join d in _context.Doctors on BP.DoctorId equals d.Id
                 select new HGSModel.Bedpatient
                 {
                     Id = BP.Id,
                     BedId = BP.BedId,
                     PatientId = BP.PatientId,
                     PatientName = p.Name + " " + p.Lastname,
                     Reason = BP.Reason,
                     State = BP.State,
                     DoctorId = BP.DoctorId,
                     DoctorName = d.Name + " " + d.Lastname,
                     Annotations = BP.Annotations,
                     StartDate = BP.StartDate,
                     EndDate = BP.EndDate
                 }).ToListAsync();

            return bedpatients;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetBPD")]
        [HttpGet]
        public async Task<HGSModel.Bedpatient> GetBPD()
        {
            IEnumerable<HGSModel.Bed> beds = await
                (from b in _context.Beds
                 where b.State == false
                 select new HGSModel.Bed
                 {
                     Id = b.Id
                 }).ToListAsync();

            IEnumerable<HGSModel.Patient> patients = await _context.Patients.Select(s =>
            new HGSModel.Patient
            {
                Id = s.Id,
                Dpi = s.Dpi
            }).ToListAsync();

            IEnumerable<HGSModel.Doctor> doctors = await _context.Doctors.Select(s =>
            new HGSModel.Doctor
            {
                Id = s.Id,
                CollegiateNumber = s.CollegiateNumber
            }).ToListAsync();

            HGSModel.Bedpatient bedpatient = new()
            {
                Beds = beds.Select(b => new SelectListItem()
                {
                    Value = b.Id.ToString(),
                    Text = b.Id.ToString()
                }).ToList(),

                Patients = patients.Select(p => new SelectListItem()
                {
                    Value = p.Id.ToString(),
                    Text = p.Dpi
                }).ToList(),

                Doctors = doctors.Select(p => new SelectListItem()
                {
                    Value = p.Id.ToString(),
                    Text = p.CollegiateNumber
                }).ToList(),

                Bed = (from b in _context.Beds
                       join AS in _context.Areasucursals on b.AreaSucursalId equals AS.Id
                       join a in _context.Areas on AS.AreaId equals a.Id
                       join m in _context.Branches on AS.BranchId equals m.Id
                       where b.Id == beds.First().Id
                       select new HGSModel.Bed
                       {
                           Size = b.Size,
                           Annotations = b.Annotations,
                           AreaSucursalName = a.Name + "_" + m.Municipality
                       }).First(),

                Patient = (from p in _context.Patients
                           where p.Id == patients.First().Id
                           select new HGSModel.Patient
                           {
                               Dpi = p.Dpi,
                               Name = p.Name,
                               Lastname = p.Lastname,
                               Birthdate = p.Birthdate,
                               Observations = p.Observations
                           }).First(),

                Doctor = (from d in _context.Doctors
                          join s in _context.Specialities on d.SpecialtyId equals s.Id
                          where d.Id == doctors.First().Id
                          select new HGSModel.Doctor
                          {
                              CollegiateNumber = d.CollegiateNumber,
                              Dpi = d.Dpi,
                              Name = d.Name,
                              Lastname = d.Lastname,
                              Birthdate = d.Birthdate,
                              SpecialtyName = s.Name
                          }).First(),
            };
            return bedpatient;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetBPDSet")]
        [HttpPost]
        public async Task<HGSModel.Bedpatient> GetBPDSet(HGSModel.Bedpatient aBedpatient)
        {
            IEnumerable<HGSModel.Bed> beds = await
                (from b in _context.Beds
                 where b.State == false
                 select new HGSModel.Bed
                 {
                     Id = b.Id
                 }).ToListAsync();

            IEnumerable<HGSModel.Patient> patients = await _context.Patients.Select(s =>
            new HGSModel.Patient
            {
                Id = s.Id,
                Dpi = s.Dpi
            }).ToListAsync();

            IEnumerable<HGSModel.Doctor> doctors = await _context.Doctors.Select(s =>
            new HGSModel.Doctor
            {
                Id = s.Id,
                CollegiateNumber = s.CollegiateNumber
            }).ToListAsync();

            HGSModel.Bedpatient bedpatient = new()
            {
                Beds = beds.Select(b => new SelectListItem()
                {
                    Value = b.Id.ToString(),
                    Text = b.Id.ToString()
                }).ToList(),

                Patients = patients.Select(p => new SelectListItem()
                {
                    Value = p.Id.ToString(),
                    Text = p.Dpi
                }).ToList(),

                Doctors = doctors.Select(p => new SelectListItem()
                {
                    Value = p.Id.ToString(),
                    Text = p.CollegiateNumber
                }).ToList(),

                Bed = (from b in _context.Beds
                       join AS in _context.Areasucursals on b.AreaSucursalId equals AS.Id
                       join a in _context.Areas on AS.AreaId equals a.Id
                       join m in _context.Branches on AS.BranchId equals m.Id
                       where b.Id == aBedpatient.BedId
                       select new HGSModel.Bed
                       {
                           Size = b.Size,
                           Annotations = b.Annotations,
                           AreaSucursalName = a.Name + "_" + m.Municipality
                       }).First(),

                Patient = (from p in _context.Patients
                           where p.Id == aBedpatient.PatientId
                           select new HGSModel.Patient
                           {
                               Dpi = p.Dpi,
                               Name = p.Name,
                               Lastname = p.Lastname,
                               Birthdate = p.Birthdate,
                               Observations = p.Observations
                           }).First(),

                Doctor = (from d in _context.Doctors
                          join s in _context.Specialities on d.SpecialtyId equals s.Id
                          where d.Id == aBedpatient.DoctorId
                          select new HGSModel.Doctor
                          {
                              CollegiateNumber = d.CollegiateNumber,
                              Dpi = d.Dpi,
                              Name = d.Name,
                              Lastname = d.Lastname,
                              Birthdate = d.Birthdate,
                              SpecialtyName = s.Name
                          }).First(),
            };
            return bedpatient;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(HGSModel.Bedpatient newBedpatient)
        {
            HGSModel.GeneralResult generalResult = new();

            try
            {
                // Validar si este Paciente ya tiene una cita y no ha sido dado de alta
                generalResult.Message = "PatientAppointment";
                if (!_context.Bedpatients.Any(bp => bp.PatientId == newBedpatient.PatientId && bp.EndDate == null))
                {
                    // Validar si el Doctor tiene más de 2 Citas
                    generalResult.Message = "BusyDoctor";
                    if (_context.Bedpatients.Count(bp => bp.DoctorId == newBedpatient.DoctorId && bp.State == false) < 2)
                    {
                        Models.Bedpatient bedpatient = new()
                        {
                            BedId = newBedpatient.BedId,
                            PatientId = newBedpatient.PatientId,
                            Reason = newBedpatient.Reason,
                            DoctorId = newBedpatient.DoctorId,
                            StartDate = newBedpatient.StartDate
                        };

                        _context.Bedpatients.Add(bedpatient);
                        await _context.SaveChangesAsync();
                        generalResult.Message = "Success";

                        // Cambiar el estado de la Cama del Paciente a -> ocupada
                        var bed = await _context.Beds.FindAsync(bedpatient.BedId);

                        if (bed != null)
                        {
                            bed.State = true;
                            _context.Beds.Update(bed);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception)
            {
                generalResult.Message = "Error";
            }
            return generalResult;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<HGSModel.Bedpatient> Get(int id)
        {
            IEnumerable<HGSModel.Bed> beds = await _context.Beds.Select(b =>
            new HGSModel.Bed
            {
                Id = b.Id
            }).ToListAsync();

            IEnumerable<HGSModel.Patient> patients = await _context.Patients.Select(p =>
            new HGSModel.Patient
            {
                Id = p.Id,
                Name = p.Name,
                Lastname = p.Lastname
            }).ToListAsync();

            IEnumerable<HGSModel.Doctor> doctors = await _context.Doctors.Select(d =>
            new HGSModel.Doctor
            {
                Id = d.Id,
                Name = d.Name,
                Lastname = d.Lastname
            }).ToListAsync();

            HGSModel.Bedpatient bedpatient = await
                (from BP in _context.Bedpatients
                 join p in _context.Patients on BP.PatientId equals p.Id
                 join d in _context.Doctors on BP.DoctorId equals d.Id
                 where BP.Id == id
                 select new HGSModel.Bedpatient
                 {
                     Id = BP.Id,
                     BedId = BP.BedId,
                     PatientName = p.Name + " " + p.Lastname,
                     PatientId = BP.PatientId,
                     Reason = BP.Reason,
                     State = BP.State,
                     DoctorName = d.Name + " " + d.Lastname,
                     DoctorId = BP.DoctorId,
                     Annotations = BP.Annotations,
                     StartDate = BP.StartDate,
                     EndDate = BP.EndDate
                 }).FirstAsync();

            bedpatient.Beds = beds.Select(b => new SelectListItem()
            {
                Value = b.Id.ToString(),
                Text = b.Id.ToString(),
                Selected = (b.Id == bedpatient.BedId)
            }).ToList();

            bedpatient.Patients = patients.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Name + " " + p.Lastname,
                Selected = (p.Id == bedpatient.PatientId)
            }).ToList();

            bedpatient.Doctors = doctors.Select(d => new SelectListItem()
            {
                Value = d.Id.ToString(),
                Text = d.Name + " " + d.Lastname,
                Selected = (d.Id == bedpatient.DoctorId)
            }).ToList();

            return bedpatient;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(HGSModel.Bedpatient updatedBedpatient)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Message = "Unsuccessfully"
            };

            try
            {
                var bedpatient = await _context.Bedpatients.FindAsync(updatedBedpatient.Id);
                if (bedpatient != null)
                {
                    if (updatedBedpatient.Annotations != "")
                    {
                        bedpatient.Annotations += " | " + updatedBedpatient.Annotations;
                    }                    
                    bedpatient.State = updatedBedpatient.State;
                    bedpatient.EndDate = updatedBedpatient.EndDate;

                    _context.Bedpatients.Update(bedpatient);
                    await _context.SaveChangesAsync();

                    // Si dio de baja al paciente, hay que desocupar la cama
                    if (bedpatient.State)
                    {
                        var bed = await _context.Beds.FindAsync(bedpatient.BedId);

                        if (bed != null)
                        {
                            bed.State = false;                            
                            _context.Beds.Update(bed);
                            await _context.SaveChangesAsync();
                        }
                    }

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