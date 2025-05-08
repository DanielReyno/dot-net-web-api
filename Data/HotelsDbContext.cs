using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPITesting.Data.Config;

namespace WebAPITesting.Data
{
    public class HotelsDbContext : IdentityDbContext<UserAccount>
    {
        public HotelsDbContext(DbContextOptions<HotelsDbContext> options) : base(options)
        {
            
        }

        //DbSet son metodos que se encargaran de crear las tablas correspondientes a las entidades definidas.
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        //Esto es un Seed Data para llenar la tabla con datos a la hora de crear la tabla en la base de datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
        }

    }
}
