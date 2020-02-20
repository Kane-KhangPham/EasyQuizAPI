using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyQuizApi.Data.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaoViens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoViens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KiThis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiThis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonHocs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CauHois",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    MonHocId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GiaoVienId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHois_GiaoViens_GiaoVienId",
                        column: x => x.GiaoVienId,
                        principalTable: "GiaoViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CauHois_MonHocs_MonHocId",
                        column: x => x.MonHocId,
                        principalTable: "MonHocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Des",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MonHocId = table.Column<int>(nullable: false),
                    SoCau = table.Column<int>(nullable: false),
                    ThoiGian = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    KyThiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Des", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Des_KiThis_KyThiId",
                        column: x => x.KyThiId,
                        principalTable: "KiThis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Des_MonHocs_MonHocId",
                        column: x => x.MonHocId,
                        principalTable: "MonHocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiaoVienMonHocs",
                columns: table => new
                {
                    GiaoVienId = table.Column<int>(nullable: false),
                    MonHocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoVienMonHocs", x => new { x.MonHocId, x.GiaoVienId });
                    table.ForeignKey(
                        name: "FK_GiaoVienMonHocs_GiaoViens_GiaoVienId",
                        column: x => x.GiaoVienId,
                        principalTable: "GiaoViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiaoVienMonHocs_MonHocs_MonHocId",
                        column: x => x.MonHocId,
                        principalTable: "MonHocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    GiaoVienId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_GiaoViens_GiaoVienId",
                        column: x => x.GiaoVienId,
                        principalTable: "GiaoViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CauHoiId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IsAnswer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_CauHois_CauHoiId",
                        column: x => x.CauHoiId,
                        principalTable: "CauHois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeCauHois",
                columns: table => new
                {
                    DeId = table.Column<int>(nullable: false),
                    CauHoiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeCauHois", x => new { x.CauHoiId, x.DeId });
                    table.ForeignKey(
                        name: "FK_DeCauHois_CauHois_CauHoiId",
                        column: x => x.CauHoiId,
                        principalTable: "CauHois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeCauHois_Des_DeId",
                        column: x => x.DeId,
                        principalTable: "Des",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeThis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeId = table.Column<int>(nullable: false),
                    SoDe = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    ThuTuCauHoi = table.Column<string>(type: "ntext", nullable: true),
                    KyThiId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "SoanDes",
                columns: table => new
                {
                    GiaoVienId = table.Column<int>(nullable: false),
                    DeId = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoanDes", x => new { x.DeId, x.GiaoVienId });
                    table.ForeignKey(
                        name: "FK_SoanDes_Des_DeId",
                        column: x => x.DeId,
                        principalTable: "Des",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoanDes_GiaoViens_GiaoVienId",
                        column: x => x.GiaoVienId,
                        principalTable: "GiaoViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_GiaoVienId",
                table: "Accounts",
                column: "GiaoVienId",
                unique: true,
                filter: "[GiaoVienId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CauHois_GiaoVienId",
                table: "CauHois",
                column: "GiaoVienId");

            migrationBuilder.CreateIndex(
                name: "IX_CauHois_MonHocId",
                table: "CauHois",
                column: "MonHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DeCauHois_DeId",
                table: "DeCauHois",
                column: "DeId");

            migrationBuilder.CreateIndex(
                name: "IX_Des_KyThiId",
                table: "Des",
                column: "KyThiId");

            migrationBuilder.CreateIndex(
                name: "IX_Des_MonHocId",
                table: "Des",
                column: "MonHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DeThis_DeId",
                table: "DeThis",
                column: "DeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeThis_KyThiId",
                table: "DeThis",
                column: "KyThiId");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoVienMonHocs_GiaoVienId",
                table: "GiaoVienMonHocs",
                column: "GiaoVienId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_CauHoiId",
                table: "Options",
                column: "CauHoiId");

            migrationBuilder.CreateIndex(
                name: "IX_SoanDes_GiaoVienId",
                table: "SoanDes",
                column: "GiaoVienId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "DeCauHois");

            migrationBuilder.DropTable(
                name: "DeThis");

            migrationBuilder.DropTable(
                name: "GiaoVienMonHocs");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "SoanDes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "Des");

            migrationBuilder.DropTable(
                name: "GiaoViens");

            migrationBuilder.DropTable(
                name: "KiThis");

            migrationBuilder.DropTable(
                name: "MonHocs");
        }
    }
}
