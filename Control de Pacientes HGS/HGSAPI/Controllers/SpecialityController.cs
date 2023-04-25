using HGSAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialityController : Controller
    {
        private readonly HgsContext _context = new();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<HGSModel.Speciality>> GetList()
        {
            IEnumerable<HGSModel.Speciality> specialities = await
                (from s in _context.Specialities
                 select new HGSModel.Speciality
                 {
                     Id = s.Id,
                     Name = s.Name,
                 }).ToListAsync();
            
            return specialities;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(HGSModel.Speciality newSpeciality)
        {
            HGSModel.GeneralResult generalResult = new()
            {                
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Specialities.Any(c => c.Name.ToLower() == newSpeciality.Name.ToLower()))
                {
                    Speciality speciality = new()
                    {
                        Name = newSpeciality.Name
                    };

                    _context.Specialities.Add(speciality);
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<HGSModel.Speciality?> Get(int id)
        {
            HGSModel.Speciality speciality = await
                (from s in _context.Specialities
                 select new HGSModel.Speciality
                 {
                     Id = s.Id,
                     Name = s.Name
                 }).FirstAsync(s => s.Id == id);

            return speciality;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(HGSModel.Speciality updatedSpeciality)
        {
            HGSModel.GeneralResult generalResult = new()
            {                
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Specialities.Any(c => c.Name.ToLower() == updatedSpeciality.Name.ToLower() && c.Id != updatedSpeciality.Id))
                {
                    var speciality = await _context.Specialities.FindAsync(updatedSpeciality.Id);
                    if (speciality != null)
                    {
                        speciality.Name = updatedSpeciality.Name;

                        _context.Specialities.Update(speciality);
                        await _context.SaveChangesAsync();                        
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
