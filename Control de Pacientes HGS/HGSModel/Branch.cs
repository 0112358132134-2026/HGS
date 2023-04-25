using System.ComponentModel.DataAnnotations;

namespace HGSModel
{
    public class Branch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de la Sucursal")]
        public string Municipality { get; set; } = null!;
    }
}