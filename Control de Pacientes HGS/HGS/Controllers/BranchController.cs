using HGS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGS.Controllers
{
    public class BranchController : Controller
    {
        private readonly HgsContext _context;

        public BranchController()
        {
            _context = new HgsContext();
        }
        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var branches = await _context.Branches.ToListAsync();
            return View(branches);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string municipality)
        {            
            if (!_context.Branches.Any(c => c.Municipality.ToLower() == municipality.ToLower()))
            {
                Branch branch = new()
                {
                    Municipality = municipality
                };
                _context.Branches.Add(branch);
                _context.SaveChanges();
                @ViewData["Result"] = "OK";
            }
            else
            {
                @ViewData["Result"] = "Error";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            return View(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string municipality)
        {
            if (!_context.Branches.Any(c => c.Municipality.ToLower() == municipality.ToLower() && c.Id != id))
            {
                var branch = await _context.Branches.FindAsync(id);
                if (branch != null)
                {
                    branch.Municipality = municipality;
                    _context.Update(branch);
                    _context.SaveChanges();
                    @ViewData["Result"] = "OK";
                }
            }                                        
            else
            {
                @ViewData["Result"] = "Error";
            }
            return View();
        }
    }
}