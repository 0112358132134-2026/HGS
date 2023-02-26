using HGS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class AreaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            HgsContext _HGSContext = new HgsContext();
            IEnumerable<HGSModel.Area> areas = (from c in _HGSContext.Areas
                                                select new HGSModel.Area
                                                {
                                                    Id = c.Id,
                                                    Name = c.Name,
                                                    Description = c.Description                                                    
                                                }).ToList();
            
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
            HgsContext _hgsContext = new();

            IEnumerable<HGSModel.Area> _areas = (from c in _hgsContext.Areas
                                                 where c.Name.ToLower() == name.ToLower()
                                                 select new HGSModel.Area
                                                 {
                                                     Name = c.Name,
                                                 }).ToList();

            if (!_areas.Any())
            {
                Area area = new()
                {
                    Name = name,
                    Description = description
                };
                _hgsContext.Areas.Add(area);
                _hgsContext.SaveChanges();
                @ViewData["Resultado"] = "¡Área agregada exitosamente!";
            }
            else
            {
                @ViewData["Resultado"] = "El área ya ha sido ingresada...";
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HgsContext _hgsContext = new();
            Area area = _hgsContext.Areas.FirstOrDefault(c => c.Id == id);
            HGSModel.Area areaResult = new()
            {
                Name = area.Name, 
                Description = area.Description
            };
            return View(areaResult);
        }

        [HttpPost]
        public IActionResult Edit(int id, string name, string description)
        {
            HgsContext _hgsContext = new();
            Area area = _hgsContext.Areas.FirstOrDefault(s => s.Id == id);
            area.Name = name;
            area.Description = description;
            _hgsContext.SaveChanges();
            return View();
        }
    }
}