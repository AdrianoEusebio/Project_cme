using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CME_api.Migrations
{
    /// <inheritdoc />
    public partial class InsertRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Admin" },
                    { "Enfermeiro" }
                });

            string passwordHash = BCrypt.Net.BCrypt.HashPassword("admin123");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "HashPassword", "Email", "CriadoEm", "IdGroup" },
                values: new object[]
                {
                "superadmin", passwordHash, "admin@admin.com", DateTime.UtcNow, 1
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users WHERE Username = 'superadmin'");
            migrationBuilder.Sql("DELETE FROM UserGroups WHERE Name IN ('Admin', 'Enfermeiro')");
        }
    }
}
