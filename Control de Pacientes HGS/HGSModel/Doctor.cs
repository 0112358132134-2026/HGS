using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HGSModel
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el número de colegiado del Doctor")]
        [RegularExpression(@"^[1-9]\d{8}$", ErrorMessage = "Ingrese un colegiado válido")]
        public string CollegiateNumber { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el nombre de usuario del Doctor")]
        public string User { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la contraseña del Doctor")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Verifique la contraseña del Doctor")]
        public string ConfirmedPassword { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el DPI del Doctor")]
        [RegularExpression(@"^[1-9]\d{12}$", ErrorMessage = "Ingrese un DPI válido")]
        public string Dpi { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el nombre del Doctor")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el apellido del Doctor")]
        public string Lastname { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la fecha de nacimiento del Doctor")]
        public DateTime Birthdate { get; set; }
        
        public int SpecialtyId { get; set; }

        public string? SpecialtyName { get; set; }

        public virtual Speciality? Specialty { get; set; }

        public List<SelectListItem>? Specialities { get; set; }
    }
}