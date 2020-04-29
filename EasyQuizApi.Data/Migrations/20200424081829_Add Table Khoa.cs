using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class AddTableKhoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_GiaoViens_GiaoVienId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CauHois_GiaoViens_GiaoVienId",
                table: "CauHois");

            migrationBuilder.DropForeignKey(
                name: "FK_CauHois_MonHocs_MonHocId",
                table: "CauHois");

            migrationBuilder.DropForeignKey(
                name: "FK_Des_KiThis_KyThiId",
                table: "Des");

            migrationBuilder.DropForeignKey(
                name: "FK_Des_Lops_LopId",
                table: "Des");

            migrationBuilder.DropForeignKey(
                name: "FK_Des_MonHocs_MonHocId",
                table: "Des");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoVienMonHocs_GiaoViens_GiaoVienId",
                table: "GiaoVienMonHocs");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoVienMonHocs_MonHocs_MonHocId",
                table: "GiaoVienMonHocs");

            migrationBuilder.DropForeignKey(
                name: "FK_SoanDes_GiaoViens_GiaoVienId",
                table: "SoanDes");

            migrationBuilder.AddColumn<int>(
                name: "KhoaId",
                table: "GiaoViens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_GiaoViens_GiaoVienId",
                table: "Accounts",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CauHois_GiaoViens_GiaoVienId",
                table: "CauHois",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CauHois_MonHocs_MonHocId",
                table: "CauHois",
                column: "MonHocId",
                principalTable: "MonHocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Des_KiThis_KyThiId",
                table: "Des",
                column: "KyThiId",
                principalTable: "KiThis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Des_Lops_LopId",
                table: "Des",
                column: "LopId",
                principalTable: "Lops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Des_MonHocs_MonHocId",
                table: "Des",
                column: "MonHocId",
                principalTable: "MonHocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoVienMonHocs_GiaoViens_GiaoVienId",
                table: "GiaoVienMonHocs",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoVienMonHocs_MonHocs_MonHocId",
                table: "GiaoVienMonHocs",
                column: "MonHocId",
                principalTable: "MonHocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoanDes_GiaoViens_GiaoVienId",
                table: "SoanDes",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_GiaoViens_GiaoVienId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CauHois_GiaoViens_GiaoVienId",
                table: "CauHois");

            migrationBuilder.DropForeignKey(
                name: "FK_CauHois_MonHocs_MonHocId",
                table: "CauHois");

            migrationBuilder.DropForeignKey(
                name: "FK_Des_KiThis_KyThiId",
                table: "Des");

            migrationBuilder.DropForeignKey(
                name: "FK_Des_Lops_LopId",
                table: "Des");

            migrationBuilder.DropForeignKey(
                name: "FK_Des_MonHocs_MonHocId",
                table: "Des");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoVienMonHocs_GiaoViens_GiaoVienId",
                table: "GiaoVienMonHocs");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoVienMonHocs_MonHocs_MonHocId",
                table: "GiaoVienMonHocs");

            migrationBuilder.DropForeignKey(
                name: "FK_SoanDes_GiaoViens_GiaoVienId",
                table: "SoanDes");

            migrationBuilder.DropTable(
                name: "Khoas");

            migrationBuilder.DropColumn(
                name: "KhoaId",
                table: "GiaoViens");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_GiaoViens_GiaoVienId",
                table: "Accounts",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CauHois_GiaoViens_GiaoVienId",
                table: "CauHois",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CauHois_MonHocs_MonHocId",
                table: "CauHois",
                column: "MonHocId",
                principalTable: "MonHocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Des_KiThis_KyThiId",
                table: "Des",
                column: "KyThiId",
                principalTable: "KiThis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Des_Lops_LopId",
                table: "Des",
                column: "LopId",
                principalTable: "Lops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Des_MonHocs_MonHocId",
                table: "Des",
                column: "MonHocId",
                principalTable: "MonHocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoVienMonHocs_GiaoViens_GiaoVienId",
                table: "GiaoVienMonHocs",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoVienMonHocs_MonHocs_MonHocId",
                table: "GiaoVienMonHocs",
                column: "MonHocId",
                principalTable: "MonHocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SoanDes_GiaoViens_GiaoVienId",
                table: "SoanDes",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
