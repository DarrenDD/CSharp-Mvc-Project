using Microsoft.EntityFrameworkCore.Migrations;

namespace SpringOceanTechnologiesIMS.Migrations
{
    public partial class addedProgrammesToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProgrammes_AspNetUsers_UserId",
                table: "UserProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgrammes_Programmes_ProgrammeId",
                table: "UserProgrammes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProgrammes",
                table: "UserProgrammes");

            migrationBuilder.RenameTable(
                name: "UserProgrammes",
                newName: "UserProgramme");

            migrationBuilder.RenameIndex(
                name: "IX_UserProgrammes_UserId",
                table: "UserProgramme",
                newName: "IX_UserProgramme_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProgrammes_ProgrammeId",
                table: "UserProgramme",
                newName: "IX_UserProgramme_ProgrammeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProgramme",
                table: "UserProgramme",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgramme_AspNetUsers_UserId",
                table: "UserProgramme",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgramme_Programmes_ProgrammeId",
                table: "UserProgramme",
                column: "ProgrammeId",
                principalTable: "Programmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProgramme_AspNetUsers_UserId",
                table: "UserProgramme");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgramme_Programmes_ProgrammeId",
                table: "UserProgramme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProgramme",
                table: "UserProgramme");

            migrationBuilder.RenameTable(
                name: "UserProgramme",
                newName: "UserProgrammes");

            migrationBuilder.RenameIndex(
                name: "IX_UserProgramme_UserId",
                table: "UserProgrammes",
                newName: "IX_UserProgrammes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProgramme_ProgrammeId",
                table: "UserProgrammes",
                newName: "IX_UserProgrammes_ProgrammeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProgrammes",
                table: "UserProgrammes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgrammes_AspNetUsers_UserId",
                table: "UserProgrammes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgrammes_Programmes_ProgrammeId",
                table: "UserProgrammes",
                column: "ProgrammeId",
                principalTable: "Programmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
