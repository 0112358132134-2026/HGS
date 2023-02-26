using System.ComponentModel.DataAnnotations;

namespace HGSModel
{
    public class Branch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del Municipio")]
        public string Municipality { get; set; } = null!;
    }
}