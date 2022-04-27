using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class database_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentProduct_MedicalProducts_MedicalProductProductID",
                table: "CommentProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalProducts_AspNetUsers_AppUserId",
                table: "MedicalProducts");

            migrationBuilder.DropIndex(
                name: "IX_MedicalProducts_AppUserId",
                table: "MedicalProducts");

            migrationBuilder.DropIndex(
                name: "IX_CommentProduct_MedicalProductProductID",
                table: "CommentProduct");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AppUserId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "MedicalProducts");

            migrationBuilder.DropColumn(
                name: "MedicalProductProductID",
                table: "CommentProduct");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Articles");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProducts_UserID",
                table: "MedicalProducts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentProduct_ProductID",
                table: "CommentProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserID",
                table: "Articles",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserID",
                table: "Articles",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_UserID",
                table: "Blogs",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentProduct_MedicalProducts_ProductID",
                table: "CommentProduct",
                column: "ProductID",
                principalTable: "MedicalProducts",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalProducts_AspNetUsers_UserID",
                table: "MedicalProducts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserID",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_UserID",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentProduct_MedicalProducts_ProductID",
                table: "CommentProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalProducts_AspNetUsers_UserID",
                table: "MedicalProducts");

            migrationBuilder.DropIndex(
                name: "IX_MedicalProducts_UserID",
                table: "MedicalProducts");

            migrationBuilder.DropIndex(
                name: "IX_CommentProduct_ProductID",
                table: "CommentProduct");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserID",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "MedicalProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalProductProductID",
                table: "CommentProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProducts_AppUserId",
                table: "MedicalProducts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentProduct_MedicalProductProductID",
                table: "CommentProduct",
                column: "MedicalProductProductID");

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
                name: "FK_CommentProduct_MedicalProducts_MedicalProductProductID",
                table: "CommentProduct",
                column: "MedicalProductProductID",
                principalTable: "MedicalProducts",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalProducts_AspNetUsers_AppUserId",
                table: "MedicalProducts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
