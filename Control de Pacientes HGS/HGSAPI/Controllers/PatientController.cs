using HGSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HGSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : Controller
    {
        private readonly HgsContext _context = new();

        // Devuelve lista de Pacientes
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<Patient>> GetList()
        {
            IEnumerable<Patient> patients = await _context.Patients.ToListAsync();
            return patients;
        }
    }
}