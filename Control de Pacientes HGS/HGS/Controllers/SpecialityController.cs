using HGS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class SpecialityController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            IEnumerable<HGSModel.Speciality>? specialities = await APIService<HGSModel.Speciality>.GetList("Speciality/GetList", token._token);
            return View(specialities);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] HGSModel.Speciality newSpeciality)
        {
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newSpeciality, "Speciality/Set", token._token);

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            HGSModel.Speciality? speciality = await APIService<HGSModel.Speciality>.Get(id, "Speciality/Get/", token._token);
            return View(speciality);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Name")] HGSModel.Speciality updatedSpeciality)
        {
            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedSpeciality, "Speciality/Update", token._token);

            if (generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }

            return View();
        }
    }
}