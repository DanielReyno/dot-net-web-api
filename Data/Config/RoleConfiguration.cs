using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPITesting.Data.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
                (
                    new IdentityRole
                    {
                        Id = "8af39f49-3644-4fc3-9b1e-47e8f9c9ea21",
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    },
                    new IdentityRole
                    {
                        Id = "88afcdd0-149d-4d00-8eb0-c24c34e4422f",
                        Name = "User",
                        NormalizedName = "USER"
                    }
                ) ;
        }
    }
}
