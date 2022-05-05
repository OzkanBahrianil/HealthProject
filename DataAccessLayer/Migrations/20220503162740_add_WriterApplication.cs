using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class add_WriterApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WriterApplication",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationCV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationCoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUniversity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUniversityDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationStatus = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriterApplication", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK_WriterApplication_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WriterApplication_UserID",
                table: "WriterApplication",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WriterApplication");
        }
    }
}
