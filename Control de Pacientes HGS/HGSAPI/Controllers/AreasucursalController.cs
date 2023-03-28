using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreasucursalController : Controller
    {
        private readonly HgsContext _context = new();

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<HGSModel.Areasucursal>> GetList()
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
            return areasucursals;
        }

        [Route("GetAB")]
        [HttpGet]
        public async Task<HGSModel.Areasucursal> GetAB()
        {
            IEnumerable<HGSModel.Area> areas = await _context.Areas.Select(s =>
            new HGSModel.Area
            {
                Id = s.Id,
                Name = s.Name,
            }).ToListAsync();

            IEnumerable<HGSModel.Branch> branches = await _context.Branches.Select(s =>
            new HGSModel.Branch
            {
                Id = s.Id,
                Municipality = s.Municipality
            }).ToListAsync();

            HGSModel.Areasucursal areasucursal = new()
            {
                Areas = areas.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),
                Branches = branches.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Municipality
                }).ToList(),

                //Movie = (from m in _context.Movies
                //         where m.Id == movies.First().Id
                //         select new ClasificacionPeliculasModel.Movie
                //         {
                //             Title = m.Title,
                //             ReleaseDate = m.ReleaseDate,
                //             Duration = m.Duration,
                //             Director = m.Director,
                //             Actors = m.Actors

                //         }).First()
            };
            return areasucursal;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(Areasucursal newAreasucursal)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Areasucursals.Any(c => c.AreaId == newAreasucursal.AreaId && c.BranchId == newAreasucursal.BranchId))
                {
                    _context.Areasucursals.Add(newAreasucursal);
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
        public async Task<HGSModel.Areasucursal> Get(int id)
        {
            IEnumerable<HGSModel.Area> areas = await _context.Areas.Select(s =>
            new HGSModel.Area
            {
                Id = s.Id,
                Name = s.Name,
            }).ToListAsync();

            IEnumerable<HGSModel.Branch> branches = await _context.Branches.Select(s =>
            new HGSModel.Branch
            {
                Id = s.Id,
                Municipality = s.Municipality
            }).ToListAsync();

            HGSModel.Areasucursal areasucursal = await
                (from AS in _context.Areasucursals
                 join a in _context.Areas on AS.AreaId equals a.Id
                 join s in _context.Branches on AS.BranchId equals s.Id
                 select new HGSModel.Areasucursal
                 {
                     Id = AS.Id,
                     AreaName = a.Name,
                     BranchName = s.Municipality,
                     AreaId = AS.AreaId,
                     BranchId = AS.BranchId
                 }).FirstAsync(AS => AS.Id == id);

            areasucursal.Areas = areas.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name,
                Selected = (s.Id == areasucursal.AreaId)
            }).ToList();
            areasucursal.Branches = branches.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Municipality,
                Selected = (s.Id == areasucursal.BranchId)
            }).ToList();

            return areasucursal;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(Areasucursal updatedAreasucursal)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Areasucursals.Any(c => c.AreaId == updatedAreasucursal.AreaId && c.BranchId == updatedAreasucursal.BranchId && c.Id != updatedAreasucursal.Id))
                {
                    var areasucursal = await _context.Areasucursals.FindAsync(updatedAreasucursal.Id);
                    if (areasucursal != null)
                    {
                        areasucursal.AreaId = updatedAreasucursal.AreaId;
                        areasucursal.BranchId = updatedAreasucursal.BranchId;

                        _context.Areasucursals.Update(areasucursal);
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