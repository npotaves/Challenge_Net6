using Challenge_Net6.Models;
using Microsoft.EntityFrameworkCore;


namespace Challenge_Net6.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Factura> Facturas { get; set; }


    }
}
