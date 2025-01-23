using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CME_api.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Group",
                columns: table => new
                {
                    id_group = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    nivel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Group", x => x.id_group);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    groupid_group = table.Column<int>(type: "integer", nullable: true),
                    hashPassword = table.Column<string>(type: "text", nullable: true),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    dataAtual = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.name);
                    table.ForeignKey(
                        name: "FK_Users_User_Group_groupid_group",
                        column: x => x.groupid_group,
                        principalTable: "User_Group",
                        principalColumn: "id_group");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_groupid_group",
                table: "Users",
                column: "groupid_group");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "User_Group");
        }
    }
}
