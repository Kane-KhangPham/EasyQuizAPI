using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class DeleteCasecase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_CauHois_CauHoiId",
                table: "Options");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_CauHois_CauHoiId",
                table: "Options",
                column: "CauHoiId",
                principalTable: "CauHois",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_CauHois_CauHoiId",
                table: "Options");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_CauHois_CauHoiId",
                table: "Options",
                column: "CauHoiId",
                principalTable: "CauHois",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
