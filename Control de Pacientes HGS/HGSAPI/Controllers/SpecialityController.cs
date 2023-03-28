using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialityController : Controller
    {
        private readonly HgsContext _context;

        public SpecialityController()
        {
            _context = new HgsContext();
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<Speciality>> GetList()
        {
            IEnumerable<Speciality> specialities = await _context.Specialities.ToListAsync();
            return specialities;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(Speciality newSpeciality)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Specialities.Any(c => c.Name.ToLower() == newSpeciality.Name.ToLower()))
                {
                    _context.Specialities.Add(newSpeciality);
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
        public async Task<Speciality?> Get(int id)
        {
            return await _context.Specialities.FindAsync(id);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(Speciality updatedSpeciality)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
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
