using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreaController : Controller
    {
        private readonly HgsContext _context = new();

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<Area>> GetList()
        {
            IEnumerable<Area> areas = await _context.Areas.ToListAsync();
            return areas;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(Area newArea)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Areas.Any(c => c.Name.ToLower() == newArea.Name.ToLower()))
                {
                    _context.Areas.Add(newArea);
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
        public async Task<Area?> Get(int id)
        {
            return await _context.Areas.FindAsync(id);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(Area updatedArea)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Areas.Any(c => c.Name.ToLower() == updatedArea.Name.ToLower() && c.Id != updatedArea.Id))
                {
                    var area = await _context.Areas.FindAsync(updatedArea.Id);
                    if (area != null)
                    {
                        area.Name = updatedArea.Name;
                        area.Description = updatedArea.Description;

                        _context.Areas.Update(area);
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