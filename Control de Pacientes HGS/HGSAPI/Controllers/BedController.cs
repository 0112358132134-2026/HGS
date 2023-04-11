using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BedController : Controller
    {
        private readonly HgsContext _context = new();

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<HGSModel.Bed>> GetList()
        {
            IEnumerable<HGSModel.Bed> beds = await
                (from B in _context.Beds
                 join AS in _context.Areasucursals on B.AreaSucursalId equals AS.Id
                 join a in _context.Areas on AS.AreaId equals a.Id
                 join s in _context.Branches on AS.BranchId equals s.Id
                 select new HGSModel.Bed
                 {
                     Id = B.Id,
                     Size = B.Size,
                     Annotations = B.Annotations,
                     State = B.State,
                     AreaSucursalName = a.Name + "_" + s.Municipality
                 }).ToListAsync();
            return beds;
        }

        [Route("GetAS")]
        [HttpGet]
        public async Task<HGSModel.Bed> GetAS()
        {
            IEnumerable<HGSModel.Areasucursal> areasucursals = await
                (from AS in _context.Areasucursals
                 join a in _context.Areas on AS.AreaId equals a.Id
                 join s in _context.Branches on AS.BranchId equals s.Id
                 select new HGSModel.Areasucursal
                 {
                     Id = AS.Id,
                     AreaName = a.Name,
                     BranchName = s.Municipality
                 }).ToListAsync();

            IEnumerable<string> sizes = new List<string>() { "Pequeña", "Mediana", "Grande", "Extra-Grande" };
            
            HGSModel.Bed bed = new()
            {
                AreaSucursals = areasucursals.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.AreaName + "_" + s.BranchName
                }).ToList(),

                Sizes = sizes.Select(s => new SelectListItem()
                {
                    Value = s,
                    Text = s,                    
                }).ToList()
            };
            return bed;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(HGSModel.Bed newBed)
        {
            HGSModel.GeneralResult generalResult = new()
            {                
                Message = "Unsuccessfully"
            };

            try
            {
                Bed bed = new()
                {
                    AreaSucursalId = newBed.AreaSucursalId,
                    Size = newBed.Size,
                    Annotations = newBed.Annotations,
                    State = false
                };

                _context.Beds.Add(bed);
                await _context.SaveChangesAsync();                
                generalResult.Message = "Success";
            }
            catch (Exception)
            {
                generalResult.Message = "Error";
            }
            return generalResult;
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<HGSModel.Bed> Get(int id)
        {
            IEnumerable<HGSModel.Areasucursal> areasucursals = await
                (from AS in _context.Areasucursals
                 join a in _context.Areas on AS.AreaId equals a.Id
                 join s in _context.Branches on AS.BranchId equals s.Id
                 select new HGSModel.Areasucursal
                 {
                     Id = AS.Id,
                     AreaName = a.Name,
                     BranchName = s.Municipality
                 }).ToListAsync();

            HGSModel.Bed bed = await
                (from B in _context.Beds
                 join AS in _context.Areasucursals on B.AreaSucursalId equals AS.Id
                 join a in _context.Areas on AS.AreaId equals a.Id
                 join s in _context.Branches on AS.BranchId equals s.Id
                 where B.Id == id
                 select new HGSModel.Bed
                 {
                     Id = B.Id,
                     AreaSucursalId = B.AreaSucursalId,
                     Size = B.Size,
                     Annotations = B.Annotations,
                     State = B.State,
                     AreaSucursalName = a.Name + "_" + s.Municipality
                 }).FirstAsync();

            bed.AreaSucursals = areasucursals.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.AreaName + "_" + s.BranchName,
                Selected = (s.Id == bed.AreaSucursalId)
            }).ToList();

            IEnumerable<string> sizes = new List<string>() { "Pequeña", "Mediana", "Grande", "Extra-Grande" };
            bed.Sizes = sizes.Select(s => new SelectListItem()
            {
                Value = s,
                Text = s,
                Selected = (s == bed.Size)
            }).ToList();

            return bed;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(HGSModel.Bed updatedBed)
        {
            HGSModel.GeneralResult generalResult = new()
            {                
                Message = "Unsuccessfully"
            };

            try
            {
                var bed = await _context.Beds.FindAsync(updatedBed.Id);
                if (bed != null)
                {
                    bed.AreaSucursalId = updatedBed.AreaSucursalId;
                    bed.Size = updatedBed.Size;
                    bed.Annotations = updatedBed.Annotations;

                    _context.Beds.Update(bed);
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