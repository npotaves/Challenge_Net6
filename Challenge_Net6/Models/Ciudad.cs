using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge_Net6.Models
{
    [Table("Ciudades")]
    public class Ciudad
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; } = String.Empty;
    }
}
