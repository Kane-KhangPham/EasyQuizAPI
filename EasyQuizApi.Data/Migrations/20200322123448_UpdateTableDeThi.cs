using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class UpdateTableDeThi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThi",
                table: "Des",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayThi",
                table: "Des");
        }
    }
}
