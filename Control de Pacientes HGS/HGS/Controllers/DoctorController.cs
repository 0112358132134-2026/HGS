using HGS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HGS.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            SelectListItem();
            return View();
        }

        #region Functions
        public void SelectListItem() 
        {
            HgsContext _HGSContext = new();

            ViewBag.SpecialtyId = (from c in _HGSContext.Specialities
                                   select new SelectListItem
                                   {
                                       Value = c.Name,
                                       Text = c.Name
                                   }).ToList();
        }
        #endregion
    }
}