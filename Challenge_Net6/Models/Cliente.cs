using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge_Net6.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; } = String.Empty;

        [StringLength(50)]
        public string Apellido { get; set; } = String.Empty;

        [StringLength(50)]
        public string Domicilio { get; set; } = String.Empty;

        [StringLength(100)]
        public string Email { get; set; } = String.Empty;

        //[StringLength(100)]
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public int CiudadId { get; set; }

        [ForeignKey("CiudadId")]
        public Ciudad Ciudad { get; set; }

        public bool Habilitado { get; set;}

       public List<Factura> Facturas { get; set; }
    }
}
