using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HGSModel
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el DPI del Paciente")]
        [RegularExpression(@"^[1-9]\d{12}$", ErrorMessage = "Ingrese un DPI válido")]
        public string Dpi { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el nombre del Paciente")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el apellido del Paciente")]
        public string Lastname { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la fecha de nacimiento del Paciente")]
        public DateTime Birthdate { get; set; }

        public string? Observations { get; set; }
    }
}