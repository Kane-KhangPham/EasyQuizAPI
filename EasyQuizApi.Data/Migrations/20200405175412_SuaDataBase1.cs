using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class SuaDataBase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "Des",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KieuDanTrang",
                table: "Des",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "Des");

            migrationBuilder.DropColumn(
                name: "KieuDanTrang",
                table: "Des");
        }
    }
}
