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
        public async Task<IEnumerable<HGSModel.Branch>> GetList()
        {
            IEnumerable<HGSModel.Branch> branches = await
                (from b in _context.Branches
                 select new HGSModel.Branch
                 {
                     Id = b.Id,
                     Municipality = b.Municipality
                 }).ToListAsync();

            return branches;
        }

        [Route("Set")]
        [HttpPost]
        public async Task<HGSModel.GeneralResult> Set(HGSModel.Branch newBranch)
        {
            HGSModel.GeneralResult generalResult = new()
            {
                Message = "Unsuccessfully"
            };

            try
            {
                if (!_context.Branches.Any(c => c.Municipality.ToLower() == newBranch.Municipality.ToLower()))
                {
                    Branch branch = new()
                    {
                        Municipality = newBranch.Municipality
                    };

                    _context.Branches.Add(branch);
                    await _context.SaveChangesAsync();
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
        public async Task<HGSModel.Branch?> Get(int id)
        {
            HGSModel.Branch branch = await
                (from b in _context.Branches
                 select new HGSModel.Branch
                 {
                     Id = b.Id,
                     Municipality = b.Municipality
                 }).FirstAsync(b => b.Id == id);

            return branch;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<HGSModel.GeneralResult> Update(HGSModel.Branch updatedBranch)
        {
            HGSModel.GeneralResult generalResult = new()
            {
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