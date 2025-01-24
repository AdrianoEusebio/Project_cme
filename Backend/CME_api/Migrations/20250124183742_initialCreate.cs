using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CME_api.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distributions",
                columns: table => new
                {
                    IdDistribution = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerialMaterial = table.Column<string>(type: "text", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sector = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributions", x => x.IdDistribution);
                });

            migrationBuilder.CreateTable(
                name: "ProcessHistories",
                columns: table => new
                {
                    IdProcess = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerialMaterial = table.Column<string>(type: "text", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    EntryData = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdReceiving = table.Column<int>(type: "integer", nullable: true),
                    IdDistribution = table.Column<int>(type: "integer", nullable: true),
                    WashingId = table.Column<int>(type: "integer", nullable: true),
                    EnumStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessHistories", x => x.IdProcess);
                });

            migrationBuilder.CreateTable(
                name: "Receivings",
                columns: table => new
                {
                    IdReceiving = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerialMaterial = table.Column<string>(type: "text", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    EntryData = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivings", x => x.IdReceiving);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    IdUserGroup = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.IdUserGroup);
                });

            migrationBuilder.CreateTable(
                name: "Washings",
                columns: table => new
                {
                    IdWashing = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerialMaterial = table.Column<string>(type: "text", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsWashed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Washings", x => x.IdWashing);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HashPassword = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdGroup = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_UserGroups_IdGroup",
                        column: x => x.IdGroup,
                        principalTable: "UserGroups",
                        principalColumn: "IdUserGroup",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    IdMaterial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Serial = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EntryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.IdMaterial);
                    table.ForeignKey(
                        name: "FK_Materials_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_SerialMaterial",
                table: "Distributions",
                column: "SerialMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_IdUser",
                table: "Materials",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Receivings_SerialMaterial",
                table: "Receivings",
                column: "SerialMaterial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdGroup",
                table: "Users",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Washings_SerialMaterial",
                table: "Washings",
                column: "SerialMaterial");

            //INSERTS PADRÕES
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
            migrationBuilder.DropTable(
                name: "Distributions");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "ProcessHistories");

            migrationBuilder.DropTable(
                name: "Receivings");

            migrationBuilder.DropTable(
                name: "Washings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.Sql("DELETE FROM Users WHERE Username = 'superadmin'");
            migrationBuilder.Sql("DELETE FROM UserGroups WHERE Name IN ('Admin', 'Enfermeiro')");
        }
    }
}
