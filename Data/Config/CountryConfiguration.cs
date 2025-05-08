using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPITesting.Data.Config
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData
                (
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
                );
        }
    }
}
