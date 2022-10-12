using Microsoft.EntityFrameworkCore.Migrations;

namespace SpringOceanTechnologiesIMS.Migrations
{
    public partial class AddedImageToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImagePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailImagePath",
                table: "AspNetUsers");
        }
    }
}
