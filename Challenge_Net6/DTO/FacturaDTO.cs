using Challenge_Net6.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Challenge_Net6.DTO
{
    public class FacturaDTO
    {
        public int Id { get; set; }

        public ClienteDTO Cliente { get; set; }

        public string Detalle { get; set; } = String.Empty;

        public decimal Importe { get; set; }

        public DateTime Fecha { get; set; }
    }
}
