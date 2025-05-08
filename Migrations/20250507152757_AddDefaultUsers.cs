using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPITesting.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "88afcdd0-149d-4d00-8eb0-c24c34e4422f", null, "User", "USER" },
                    { "8af39f49-3644-4fc3-9b1e-47e8f9c9ea21", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88afcdd0-149d-4d00-8eb0-c24c34e4422f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8af39f49-3644-4fc3-9b1e-47e8f9c9ea21");
        }
    }
}
