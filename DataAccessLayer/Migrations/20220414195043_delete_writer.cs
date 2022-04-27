using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class delete_writer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Writers_WriterId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalProducts_Writers_WriterId",
                table: "MedicalProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Writers_WriterId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Writers_WriterId1",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Messages_WriterId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_WriterId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_MedicalProducts_WriterId",
                table: "MedicalProducts");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_WriterId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "WriterId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "MedicalProducts");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Blogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriterId1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "MedicalProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Writers",
                columns: table => new
                {
                    WriterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WriterAbout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriterImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriterLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriterMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriterPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriterStatus = table.Column<bool>(type: "bit", nullable: false),
                    WriterVideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writers", x => x.WriterId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_WriterId",
                table: "Messages",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_WriterId1",
                table: "Messages",
                column: "WriterId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProducts_WriterId",
                table: "MedicalProducts",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_WriterId",
                table: "Blogs",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Writers_WriterId",
                table: "Blogs",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalProducts_Writers_WriterId",
                table: "MedicalProducts",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Writers_WriterId",
                table: "Messages",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Writers_WriterId1",
                table: "Messages",
                column: "WriterId1",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
