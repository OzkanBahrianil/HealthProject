using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class WriterApplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WriterApplication_AspNetUsers_UserID",
                table: "WriterApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WriterApplication",
                table: "WriterApplication");

            migrationBuilder.RenameTable(
                name: "WriterApplication",
                newName: "WriterApplications");

            migrationBuilder.RenameIndex(
                name: "IX_WriterApplication_UserID",
                table: "WriterApplications",
                newName: "IX_WriterApplications_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WriterApplications",
                table: "WriterApplications",
                column: "ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_WriterApplications_AspNetUsers_UserID",
                table: "WriterApplications",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WriterApplications_AspNetUsers_UserID",
                table: "WriterApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WriterApplications",
                table: "WriterApplications");

            migrationBuilder.RenameTable(
                name: "WriterApplications",
                newName: "WriterApplication");

            migrationBuilder.RenameIndex(
                name: "IX_WriterApplications_UserID",
                table: "WriterApplication",
                newName: "IX_WriterApplication_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WriterApplication",
                table: "WriterApplication",
                column: "ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_WriterApplication_AspNetUsers_UserID",
                table: "WriterApplication",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
