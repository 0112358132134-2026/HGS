﻿using HGS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGS.Controllers
{
    public class AreaController : Controller
    {
        private readonly HgsContext _context;

        public AreaController()
        {
            _context = new HgsContext();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var areas = await _context.Areas.ToListAsync();
            return View(areas);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string description)
        {
            if (!_context.Areas.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                Area area = new()
                {
                    Name = name,
                    Description = description
                };
                _context.Areas.Add(area);
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
            var area = await _context.Areas.FindAsync(id);
            return View(area);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name, string description)
        {
            if (!_context.Areas.Any(c => c.Name.ToLower() == name.ToLower() && c.Id != id)) 
            {
                var area = await _context.Areas.FindAsync(id);
                if (area != null)
                {
                    area.Name = name;
                    area.Description = description;
                    _context.Update(area);
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