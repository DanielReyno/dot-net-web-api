using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPITesting.Data.Config
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData
                (
                    new Hotel
                    {
                        Id = 1,
                        Name = "Hotel Punta cana",
                        Location = "Punta cana",
                        Price = 500,
                        CountryId = 1
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

                );
        }
    }
}
