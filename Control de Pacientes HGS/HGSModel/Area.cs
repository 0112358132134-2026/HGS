using System.ComponentModel.DataAnnotations;

namespace HGSModel
{
    public class Area
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del Área")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese una breve descripción del Área")]
        public string Description { get; set; } = null!;
    }
}