using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class SuaDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MonHocId",
                table: "Des",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LopId",
                table: "Des",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MonHocId",
                table: "Des",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LopId",
                table: "Des",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
