using HGS.Models;
using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class BranchController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<Branch> branches = await APIService.BranchGetList();
            return View(branches);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Municipality")] Branch newBranch)
        {
            HGSModel.GeneralResult generalResult = await APIService.BranchSet(newBranch);
            @ViewData["Response"] = generalResult.Message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Branch branch = await APIService.BranchGet(id);
            return View(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id","Municipality")] Branch updatedBranch)
        {
            HGSModel.GeneralResult generalResult = await APIService.BranchUpdate(updatedBranch);
            @ViewData["Response"] = generalResult.Message;
            return View();
        }
    }
}