using HGS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGS.Controllers
{
    public class SpecialityController : Controller
    {
        private readonly HgsContext _context;

        public SpecialityController()
        {
            _context = new HgsContext();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var specialities = await _context.Specialities.ToListAsync();
            return View(specialities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (!_context.Specialities.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                Speciality speciality = new()
                {
                    Name = name
                };
                _context.Specialities.Add(speciality);
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
            var speciality = await _context.Specialities.FindAsync(id);
            return View(speciality);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name)
        {
            if (!_context.Specialities.Any(c => c.Name.ToLower() == name.ToLower() && c.Id != id)) 
            {
                var speciality = await _context.Specialities.FindAsync(id);
                if (speciality != null)
                {
                    speciality.Name = name;
                    _context.Update(speciality);
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