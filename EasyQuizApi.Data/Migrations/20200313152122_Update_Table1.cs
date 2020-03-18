using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class Update_Table1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Des_Lop_LopId",
                table: "Des");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lop",
                table: "Lop");

            migrationBuilder.RenameTable(
                name: "Lop",
                newName: "Lops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lops",
                table: "Lops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Des_Lops_LopId",
                table: "Des",
                column: "LopId",
                principalTable: "Lops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Des_Lops_LopId",
                table: "Des");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lops",
                table: "Lops");

            migrationBuilder.RenameTable(
                name: "Lops",
                newName: "Lop");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lop",
                table: "Lop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Des_Lop_LopId",
                table: "Des",
                column: "LopId",
                principalTable: "Lop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
