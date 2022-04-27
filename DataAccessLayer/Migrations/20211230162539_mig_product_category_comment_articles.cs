using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_product_category_comment_articles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WriterLevel",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticlesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticlesTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticlesContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticlesShortContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticlesType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticlesPdf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticlesPublishDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticlesThumbnailImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticlesWritersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticlesStatus = table.Column<bool>(type: "bit", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    WriterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticlesID);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Writers_WriterID",
                        column: x => x.WriterID,
                        principalTable: "Writers",
                        principalColumn: "WriterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "MedicalProducts",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductShortContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductRealiseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductThumbnailImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductStatus = table.Column<bool>(type: "bit", nullable: false),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false),
                    WriterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalProducts", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_MedicalProducts_ProductCategory_ProductCategoryID",
                        column: x => x.ProductCategoryID,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalProducts_Writers_WriterID",
                        column: x => x.WriterID,
                        principalTable: "Writers",
                        principalColumn: "WriterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentProduct",
                columns: table => new
                {
                    CommentProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentProductUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentProductDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentProductContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentProductTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentProductStatus = table.Column<bool>(type: "bit", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    MedicalProductProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentProduct", x => x.CommentProductID);
                    table.ForeignKey(
                        name: "FK_CommentProduct_MedicalProducts_MedicalProductProductID",
                        column: x => x.MedicalProductProductID,
                        principalTable: "MedicalProducts",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryID",
                table: "Articles",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_WriterID",
                table: "Articles",
                column: "WriterID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentProduct_MedicalProductProductID",
                table: "CommentProduct",
                column: "MedicalProductProductID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProducts_ProductCategoryID",
                table: "MedicalProducts",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProducts_WriterID",
                table: "MedicalProducts",
                column: "WriterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "CommentProduct");

            migrationBuilder.DropTable(
                name: "MedicalProducts");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "WriterLevel",
                table: "Writers");
        }
    }
}
