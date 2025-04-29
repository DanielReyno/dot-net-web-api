using Microsoft.EntityFrameworkCore;

namespace WebAPITesting.Data
{
    public class HotelsDbContext : DbContext
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
            modelBuilder.Entity<Country>().HasData([
                new Country
                {
                    CountryId = 1,
                    Name = "Republica Dominicana",
                    ShortName = "Rep Dom",

                },
                new Country
                {
                    CountryId = 2,
                    Name = "Estados Unidos",
                    ShortName = "US",

                },
                new Country
                {
                    CountryId = 3,
                    Name = "Espana",
                    ShortName = "ES"
                }
            ]);
            modelBuilder.Entity<Hotel>().HasData([
                new Hotel
                {
                    Id = 1,
                    Name = "Hotel Punta cana",
                    Location = "Punta cana",
                    Price = 500,
                    CountryId =  1
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Los Angeles Hotel",
                    Location = "Los angeles",
                    Price = 1000,
                    CountryId = 2
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hotel Real",
                    Location = "Madrid",
                    Price = 800,
                    CountryId = 3
                }
            ]);
        }

    }
}
