using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class about_fix_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AboutImage1",
                table: "Abouts",
                newName: "AboutImage");

            migrationBuilder.RenameColumn(
                name: "AboutDetails1",
                table: "Abouts",
                newName: "AboutDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AboutImage",
                table: "Abouts",
                newName: "AboutImage1");

            migrationBuilder.RenameColumn(
                name: "AboutDetails",
                table: "Abouts",
                newName: "AboutDetails1");
        }
    }
}
