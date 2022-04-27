using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_presentation_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WriterVideoUrl",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Presentations",
                columns: table => new
                {
                    PresentationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentationStatus = table.Column<bool>(type: "bit", nullable: false),
                    PresentationImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentationSendUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentations", x => x.PresentationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Presentations");

            migrationBuilder.DropColumn(
                name: "WriterVideoUrl",
                table: "Writers");
        }
    }
}
