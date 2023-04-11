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
        public async Task<IEnumerable<HGSModel.Area>> GetList()
        {
            IEnumerable<HGSModel.Area> areas = await
                (from a in _context.Areas
                 select new HGSModel.Area
                 {
                     Id = a.Id,
                     Name = a.Name,
                     Description = a.Description
                 }).ToListAsync();

            return areas;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(HGSModel.Area newArea)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Areas.Any(c => c.Name.ToLower() == newArea.Name.ToLower()))
                {
                    Area area = new(){ 
                        Name = newArea.Name,
                        Description = newArea.Description
                    };

                    _context.Areas.Add(area);
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
        public async Task<HGSModel.Area?> Get(int id)
        {
            HGSModel.Area area = await
                (from a in _context.Areas
                 select new HGSModel.Area
                 {
                     Id = a.Id,
                     Name = a.Name,
                     Description = a.Description
                 }).FirstAsync(a => a.Id == id);

            return area;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(HGSModel.Area updatedArea)
        {
            HGSModel.GeneralResult generalResult = new()
            {
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