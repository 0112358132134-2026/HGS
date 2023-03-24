using System.ComponentModel.DataAnnotations;

namespace HGSModel
{
    public class Speciality
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de la especialidad")]
        public string Name { get; set; } = null!;
    }
}