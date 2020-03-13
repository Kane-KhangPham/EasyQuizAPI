using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class Update_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "KiThis");

            migrationBuilder.AddColumn<int>(
                name: "LopId",
                table: "Des",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Lop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DeletedStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lop", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Des_LopId",
                table: "Des",
                column: "LopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Des_Lop_LopId",
                table: "Des",
                column: "LopId",
                principalTable: "Lop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Des_Lop_LopId",
                table: "Des");

            migrationBuilder.DropTable(
                name: "Lop");

            migrationBuilder.DropIndex(
                name: "IX_Des_LopId",
                table: "Des");

            migrationBuilder.DropColumn(
                name: "LopId",
                table: "Des");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "KiThis",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
