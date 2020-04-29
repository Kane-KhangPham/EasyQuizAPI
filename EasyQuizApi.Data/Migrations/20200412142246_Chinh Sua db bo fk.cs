using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class ChinhSuadbbofk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_DeCauHois_Des_DeId",
                table: "DeCauHois");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeCauHois",
                table: "DeCauHois");

            migrationBuilder.DropIndex(
                name: "IX_DeCauHois_DeId",
                table: "DeCauHois");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DeCauHois",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeCauHois",
                table: "DeCauHois",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DeCauHois",
                table: "DeCauHois");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DeCauHois");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeCauHois",
                table: "DeCauHois",
                columns: new[] { "CauHoiId", "DeId" });

            migrationBuilder.CreateIndex(
                name: "IX_DeCauHois_DeId",
                table: "DeCauHois",
                column: "DeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeCauHois_Des_DeId",
                table: "DeCauHois",
                column: "DeId",
                principalTable: "Des",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
