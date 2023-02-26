using HGS.Models;
using HGSModel;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            HgsContext _HGSContext = new HgsContext();
            IEnumerable<HGSModel.Branch> branches = (from c in _HGSContext.Branches
                                                     select new HGSModel.Branch
                                                     {
                                                         Id = c.Id,
                                                         Municipality = c.Municipality,

                                                     }).ToList();
            
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
            HgsContext _hgsContext = new HgsContext();

            IEnumerable<HGSModel.Branch> _branches = (from c in _hgsContext.Branches
                                                      where c.Municipality.ToLower() == municipality.ToLower()
                                                      select new HGSModel.Branch
                                                      {                                                          
                                                          Municipality = c.Municipality
                                                      }).ToList();

            bool isExist = false;
            for (int i = 0; i < _branches.Count(); i++)
            {
                if (municipality.ToLower().Equals(_branches.ToList()[i].Municipality.ToLower()))
                {
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                Models.Branch branch = new()
                {
                    Municipality = municipality
                };
                _hgsContext.Branches.Add(branch);
                _hgsContext.SaveChanges();
                @ViewData["Resultado"] = "¡Municipio agregado exitosamente!";
            }
            else
            {
                @ViewData["Resultado"] = "Este municipio ya ha sido ingresado...";
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HgsContext _hgsContext = new();
            Models.Branch branch = _hgsContext.Branches.FirstOrDefault(c => c.Id == id);
            HGSModel.Branch branchResult = new()
            {
                Municipality = branch.Municipality
            };
            return View(branchResult);
        }

        [HttpPost]
        public IActionResult Edit(int id, string municipality)
        {
            HgsContext _hgsContext = new();
            Models.Branch branch = _hgsContext.Branches.FirstOrDefault(s => s.Id == id);
            branch.Municipality = municipality;
            _hgsContext.SaveChanges();
            return View();
        }
    }
}