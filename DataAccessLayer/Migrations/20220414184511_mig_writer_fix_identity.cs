using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_writer_fix_identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Writers_WriterID",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Writers_WriterID",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalProducts_Writers_WriterID",
                table: "MedicalProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Writers_ReceiverID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Writers_SenderID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Articles_WriterID",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "WriterID",
                table: "MedicalProducts",
                newName: "WriterId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalProducts_WriterID",
                table: "MedicalProducts",
                newName: "IX_MedicalProducts_WriterId");

            migrationBuilder.RenameColumn(
                name: "WriterID",
                table: "Blogs",
                newName: "WriterId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_WriterID",
                table: "Blogs",
                newName: "IX_Blogs_WriterId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "AspNetUsers",
                newName: "VideoUrl");

            migrationBuilder.RenameColumn(
                name: "WriterID",
                table: "Articles",
                newName: "UserID");

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

            migrationBuilder.AlterColumn<int>(
                name: "WriterId",
                table: "MedicalProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "MedicalProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "MedicalProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WriterId",
                table: "Blogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_WriterId",
                table: "Messages",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_WriterId1",
                table: "Messages",
                column: "WriterId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProducts_AppUserId",
                table: "MedicalProducts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AppUserId",
                table: "Blogs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId",
                table: "Blogs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Writers_WriterId",
                table: "Blogs",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalProducts_AspNetUsers_AppUserId",
                table: "MedicalProducts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalProducts_Writers_WriterId",
                table: "MedicalProducts",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverID",
                table: "Messages",
                column: "ReceiverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderID",
                table: "Messages",
                column: "SenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Writers_WriterId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalProducts_AspNetUsers_AppUserId",
                table: "MedicalProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalProducts_Writers_WriterId",
                table: "MedicalProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Writers_WriterId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Writers_WriterId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_WriterId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_WriterId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_MedicalProducts_AppUserId",
                table: "MedicalProducts");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AppUserId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "WriterId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "MedicalProducts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "MedicalProducts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "WriterId",
                table: "MedicalProducts",
                newName: "WriterID");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalProducts_WriterId",
                table: "MedicalProducts",
                newName: "IX_MedicalProducts_WriterID");

            migrationBuilder.RenameColumn(
                name: "WriterId",
                table: "Blogs",
                newName: "WriterID");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_WriterId",
                table: "Blogs",
                newName: "IX_Blogs_WriterID");

            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "AspNetUsers",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Articles",
                newName: "WriterID");

            migrationBuilder.AlterColumn<int>(
                name: "WriterID",
                table: "MedicalProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WriterID",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_WriterID",
                table: "Articles",
                column: "WriterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Writers_WriterID",
                table: "Articles",
                column: "WriterID",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Writers_WriterID",
                table: "Blogs",
                column: "WriterID",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalProducts_Writers_WriterID",
                table: "MedicalProducts",
                column: "WriterID",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Writers_ReceiverID",
                table: "Messages",
                column: "ReceiverID",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Writers_SenderID",
                table: "Messages",
                column: "SenderID",
                principalTable: "Writers",
                principalColumn: "WriterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
