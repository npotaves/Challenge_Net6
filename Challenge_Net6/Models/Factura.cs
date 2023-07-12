using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge_Net6.Models
{
    [Table("Facturas")]
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }   

        [StringLength(200)]
        public string Detalle { get; set; } = String.Empty;

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Importe { get; set; }

        public DateTime Fecha { get; set; }

    }
}
