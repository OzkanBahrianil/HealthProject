using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_fix_and_articlecategory_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryID",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticlesThumbnailImage",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Articles",
                newName: "ArticleCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CategoryID",
                table: "Articles",
                newName: "IX_Articles_ArticleCategoryID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArticlesPublishDate",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                columns: table => new
                {
                    ArticleCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticleCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticleCategoryStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategories", x => x.ArticleCategoryID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryID",
                table: "Articles",
                column: "ArticleCategoryID",
                principalTable: "ArticleCategories",
                principalColumn: "ArticleCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryID",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleCategories");

            migrationBuilder.RenameColumn(
                name: "ArticleCategoryID",
                table: "Articles",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_ArticleCategoryID",
                table: "Articles",
                newName: "IX_Articles_CategoryID");

            migrationBuilder.AlterColumn<string>(
                name: "ArticlesPublishDate",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ArticlesThumbnailImage",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryID",
                table: "Articles",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
