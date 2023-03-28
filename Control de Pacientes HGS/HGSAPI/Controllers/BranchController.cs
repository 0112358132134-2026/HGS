using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BranchController : Controller
    {
        private readonly HgsContext _context = new();

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<Branch>> GetList()
        {
            IEnumerable<Branch> branches = await _context.Branches.ToListAsync();
            return branches;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(Branch newBranch)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Branches.Any(c => c.Municipality.ToLower() == newBranch.Municipality.ToLower()))
                {
                    _context.Branches.Add(newBranch);
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
        public async Task<Branch?> Get(int id)
        {
            return await _context.Branches.FindAsync(id);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(Branch updatedBranch)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Result = false,
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Branches.Any(c => c.Municipality.ToLower() == updatedBranch.Municipality.ToLower() && c.Id != updatedBranch.Id))
                {
                    var branch = await _context.Branches.FindAsync(updatedBranch.Id);
                    if (branch != null)
                    {
                        branch.Municipality = updatedBranch.Municipality;

                        _context.Branches.Update(branch);
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