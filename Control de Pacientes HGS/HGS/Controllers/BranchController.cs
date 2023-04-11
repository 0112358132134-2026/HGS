using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class BranchController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HGSModel.Branch>? branches = await APIService<HGSModel.Branch>.GetList("Branch/GetList");
            return View(branches);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Municipality")] HGSModel.Branch newBranch)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newBranch, "Branch/Set");

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Branch? branch = await APIService<HGSModel.Branch>.Get(id, "Branch/Get/");
            return View(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Municipality")] HGSModel.Branch updatedBranch)
        {
            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedBranch, "Branch/Update");

            if(generalResult!= null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
        }
    }
}