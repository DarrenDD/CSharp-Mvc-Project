using Microsoft.EntityFrameworkCore.Migrations;

namespace SpringOceanTechnologiesIMS.Migrations
{
    public partial class EntityChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaTypeId",
                table: "MediaType",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MediaType",
                newName: "MediaTypeId");
        }
    }
}
