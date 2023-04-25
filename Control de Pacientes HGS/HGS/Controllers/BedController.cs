using HGS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class BedController : Controller
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

            IEnumerable<HGSModel.Bed>? beds = await APIService<HGSModel.Bed>.GetList("Bed/GetList", token._token);
            return View(beds);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
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

            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.SpecialGet("Bed/GetAS", token._token);
            return View(bed);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("AreaSucursalId", "Size", "Annotations")] HGSModel.Bed newBed)
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

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newBed, "Bed/Set", token._token);

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.SpecialGet("Bed/GetAS", token._token);
            return View(bed);
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

            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.Get(id, "Bed/Get/", token._token);
            return View(bed);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "AreaSucursalId", "Size", "Annotations")] HGSModel.Bed updatedBed)
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

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedBed, "Bed/Update", token._token);

            if(generalResult != null)
            {
                @ViewData["Response"] = generalResult.Message;
            }
            
            HGSModel.Bed? bed = await APIService<HGSModel.Bed>.SpecialGet("Bed/GetAS", token._token);
            return View(bed);
        }
    }
}