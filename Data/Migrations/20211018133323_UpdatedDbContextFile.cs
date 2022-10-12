using Microsoft.EntityFrameworkCore.Migrations;

namespace SpringOceanTechnologiesIMS.Migrations
{
    public partial class UpdatedDbContextFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ProgrammeItems_ProgrammeItemsId",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.RenameTable(
                name: "Contents",
                newName: "Content");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_ProgrammeItemsId",
                table: "Content",
                newName: "IX_Content_ProgrammeItemsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Content",
                table: "Content",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_ProgrammeItems_ProgrammeItemsId",
                table: "Content",
                column: "ProgrammeItemsId",
                principalTable: "ProgrammeItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_ProgrammeItems_ProgrammeItemsId",
                table: "Content");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Content",
                table: "Content");

            migrationBuilder.RenameTable(
                name: "Content",
                newName: "Contents");

            migrationBuilder.RenameIndex(
                name: "IX_Content_ProgrammeItemsId",
                table: "Contents",
                newName: "IX_Contents_ProgrammeItemsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ProgrammeItems_ProgrammeItemsId",
                table: "Contents",
                column: "ProgrammeItemsId",
                principalTable: "ProgrammeItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
