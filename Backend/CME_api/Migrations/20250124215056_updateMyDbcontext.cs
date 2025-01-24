using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CME_api.Migrations
{
    /// <inheritdoc />
    public partial class updateMyDbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Receivings_SerialMaterial",
                table: "Receivings");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_SerialMaterial",
                table: "Receivings",
                column: "SerialMaterial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Receivings_SerialMaterial",
                table: "Receivings");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_SerialMaterial",
                table: "Receivings",
                column: "SerialMaterial",
                unique: true);
        }
    }
}
