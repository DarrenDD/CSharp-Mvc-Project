using Microsoft.EntityFrameworkCore.Migrations;

namespace SpringOceanTechnologiesIMS.Migrations
{
    public partial class Add_Description_To_ProgramItem_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProgrammeItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProgrammeItems");
        }
    }
}
