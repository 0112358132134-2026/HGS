using HGS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;

namespace HGS.Controllers
{
    public class AreasucursalController : Controller
    {
        private readonly HgsContext _context;

        public AreasucursalController()
        {
            _context = new HgsContext();
        }

        [HttpGet]
        public IActionResult List() 
        {
            IEnumerable<HGSModel.Areasucursal> areasucursals =
                (from AS in _context.Areasucursals
                 join a in _context.Areas on AS.AreaId equals a.Id
                 join s in _context.Branches on AS.BranchId equals s.Id
                 select new HGSModel.Areasucursal
                 {
                     Id = AS.Id,
                     AreaName = a.Name,
                     BranchName = s.Municipality
                 }).ToList();
            return View(areasucursals);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            SelectListItem();            

            if (!((IEnumerable<dynamic>)ViewBag.AreaId).Any() && ((IEnumerable<dynamic>)ViewBag.BranchId).Any())
            {
                @ViewData["Result"] = "noAreas";
            }
            else if (((IEnumerable<dynamic>)ViewBag.AreaId).Any() && !((IEnumerable<dynamic>)ViewBag.BranchId).Any())
            {
                @ViewData["Result"] = "noBranches";
            }
            else if (!((IEnumerable<dynamic>)ViewBag.AreaId).Any() && !((IEnumerable<dynamic>)ViewBag.BranchId).Any())
            {
                @ViewData["Result"] = "noData";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(int areaid, int branchid)
        {                    
            if (!_context.Areasucursals.Any(c => c.AreaId == areaid && c.BranchId == branchid))
            {
                Areasucursal areasucursal = new()
                {
                    AreaId = areaid,
                    BranchId = branchid
                };
                _context.Areasucursals.Add(areasucursal);
                _context.SaveChanges();
                @ViewData["Result"] = "OK";
            }
            else
            {
                @ViewData["Result"] = "Error";
            }
            SelectListItem();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var areasucursal = await _context.Areasucursals.FindAsync(id);
            SelectListItem();
            return View(areasucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, int areaid, int branchid) 
        {
            if (!_context.Areasucursals.Any(c => c.AreaId == areaid && c.BranchId == branchid && c.Id != id))
            {
                var areasucursal = await _context.Areasucursals.FindAsync(id);
                if (areasucursal != null)
                {
                    areasucursal.AreaId = areaid;
                    areasucursal.BranchId = branchid;
                    _context.Update(areasucursal);
                    _context.SaveChanges();
                    @ViewData["Result"] = "OK";
                }
            }
            else
            {
                @ViewData["Result"] = "Error";
            }
            SelectListItem();
            return View();            
        }

        public void SelectListItem() 
        {
            ViewBag.AreaId = (from c in _context.Areas
                              select new SelectListItem
                              {
                                  Value = c.Id.ToString(),
                                  Text = c.Name
                              }).ToList();

            ViewBag.BranchId = (from c in _context.Branches
                                select new SelectListItem
                                {
                                    Value = c.Id.ToString(),
                                    Text = c.Municipality
                                }).ToList();
        }
    }
}