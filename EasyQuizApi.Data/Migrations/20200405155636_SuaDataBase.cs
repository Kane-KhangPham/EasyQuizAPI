using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class SuaDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeCauHois_Des_DeId",
                table: "DeCauHois");

            migrationBuilder.DropForeignKey(
                name: "FK_SoanDes_Des_DeId",
                table: "SoanDes");

            migrationBuilder.DropTable(
                name: "DeThis");

            migrationBuilder.AddColumn<int>(
                name: "RootDeId",
                table: "Des",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoDe",
                table: "Des",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DeCauHois_Des_DeId",
                table: "DeCauHois",
                column: "DeId",
                principalTable: "Des",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoanDes_Des_DeId",
                table: "SoanDes",
                column: "DeId",
                principalTable: "Des",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeCauHois_Des_DeId",
                table: "DeCauHois");

            migrationBuilder.DropForeignKey(
                name: "FK_SoanDes_Des_DeId",
                table: "SoanDes");

            migrationBuilder.DropColumn(
                name: "RootDeId",
                table: "Des");

            migrationBuilder.DropColumn(
                name: "SoDe",
                table: "Des");

            migrationBuilder.CreateTable(
                name: "DeThis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeId = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    KyThiId = table.Column<int>(nullable: true),
                    SoDe = table.Column<string>(nullable: true),
                    ThuTuCauHoi = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeThis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeThis_Des_DeId",
                        column: x => x.DeId,
                        principalTable: "Des",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeThis_KiThis_KyThiId",
                        column: x => x.KyThiId,
                        principalTable: "KiThis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeThis_DeId",
                table: "DeThis",
                column: "DeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeThis_KyThiId",
                table: "DeThis",
                column: "KyThiId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeCauHois_Des_DeId",
                table: "DeCauHois",
                column: "DeId",
                principalTable: "Des",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SoanDes_Des_DeId",
                table: "SoanDes",
                column: "DeId",
                principalTable: "Des",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
