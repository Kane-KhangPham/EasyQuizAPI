﻿// <auto-generated />
using System;
using EasyQuizApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyQuizApi.Data.Migrations
{
    [DbContext(typeof(EasyQuizDbContext))]
    [Migration("20200322123448_UpdateTableDeThi")]
    partial class UpdateTableDeThi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyQuizApi.Model.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName");

                    b.Property<int?>("GiaoVienId");

                    b.Property<string>("Password");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("GiaoVienId")
                        .IsUnique()
                        .HasFilter("[GiaoVienId] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.CauHoi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("GiaoVienId");

                    b.Property<int>("MonHocId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("GiaoVienId");

                    b.HasIndex("MonHocId");

                    b.ToTable("CauHois");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.De", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KyThiId");

                    b.Property<int>("LopId");

                    b.Property<int>("MonHocId");

                    b.Property<DateTime>("NgayThi");

                    b.Property<int>("SoCau");

                    b.Property<int>("Status");

                    b.Property<int>("ThoiGian");

                    b.HasKey("Id");

                    b.HasIndex("KyThiId");

                    b.HasIndex("LopId");

                    b.HasIndex("MonHocId");

                    b.ToTable("Des");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.DeCauHoi", b =>
                {
                    b.Property<int>("CauHoiId");

                    b.Property<int>("DeId");

                    b.HasKey("CauHoiId", "DeId");

                    b.HasIndex("DeId");

                    b.ToTable("DeCauHois");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.DeThi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeId");

                    b.Property<string>("FilePath");

                    b.Property<int?>("KyThiId");

                    b.Property<string>("SoDe");

                    b.Property<string>("ThuTuCauHoi")
                        .HasColumnName("ThuTuCauHoi")
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.HasIndex("DeId");

                    b.HasIndex("KyThiId");

                    b.ToTable("DeThis");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.GiaoVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GiaoViens");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.GiaoVienMonHoc", b =>
                {
                    b.Property<int>("MonHocId");

                    b.Property<int>("GiaoVienId");

                    b.HasKey("MonHocId", "GiaoVienId");

                    b.HasIndex("GiaoVienId");

                    b.ToTable("GiaoVienMonHocs");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.KyThi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("KiThis");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.Lop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("DeletedStatus");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Lops");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.MonHoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MonHocs");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CauHoiId");

                    b.Property<string>("Content");

                    b.Property<bool>("IsAnswer");

                    b.HasKey("Id");

                    b.HasIndex("CauHoiId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.SoanDe", b =>
                {
                    b.Property<int>("DeId");

                    b.Property<int>("GiaoVienId");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("DeId", "GiaoVienId");

                    b.HasIndex("GiaoVienId");

                    b.ToTable("SoanDes");
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.Account", b =>
                {
                    b.HasOne("EasyQuizApi.Model.Entities.GiaoVien", "GiaoVien")
                        .WithOne("Account")
                        .HasForeignKey("EasyQuizApi.Model.Entities.Account", "GiaoVienId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyQuizApi.Model.Entities.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.CauHoi", b =>
                {
                    b.HasOne("EasyQuizApi.Model.Entities.GiaoVien", "GiaoVien")
                        .WithMany()
                        .HasForeignKey("GiaoVienId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyQuizApi.Model.Entities.MonHoc", "MonHoc")
                        .WithMany("DsCauHoi")
                        .HasForeignKey("MonHocId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.De", b =>
                {
                    b.HasOne("EasyQuizApi.Model.Entities.KyThi", "KyThi")
                        .WithMany()
                        .HasForeignKey("KyThiId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyQuizApi.Model.Entities.Lop", "Lop")
                        .WithMany()
                        .HasForeignKey("LopId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyQuizApi.Model.Entities.MonHoc", "MonHoc")
                        .WithMany()
                        .HasForeignKey("MonHocId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.DeCauHoi", b =>
                {
                    b.HasOne("EasyQuizApi.Model.Entities.CauHoi", "CauHoi")
                        .WithMany("DeCauHois")
                        .HasForeignKey("CauHoiId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyQuizApi.Model.Entities.De", "De")
                        .WithMany("DeCauHois")
                        .HasForeignKey("DeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.DeThi", b =>
                {
                    b.HasOne("EasyQuizApi.Model.Entities.De", "DeGoc")
                        .WithMany("DeThi")
                        .HasForeignKey("DeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyQuizApi.Model.Entities.KyThi")
                        .WithMany("DeThi")
                        .HasForeignKey("KyThiId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.GiaoVienMonHoc", b =>
                {
                    b.HasOne("EasyQuizApi.Model.Entities.GiaoVien", "GiaoVien")
                        .WithMany("GiaoVienMonHocs")
                        .HasForeignKey("GiaoVienId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyQuizApi.Model.Entities.MonHoc", "MonHoc")
                        .WithMany("GiaoVienMonHocs")
                        .HasForeignKey("MonHocId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.Option", b =>
                {
                    b.HasOne("EasyQuizApi.Model.Entities.CauHoi", "CauHoi")
                        .WithMany("Options")
                        .HasForeignKey("CauHoiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyQuizApi.Model.Entities.SoanDe", b =>
                {
                    b.HasOne("EasyQuizApi.Model.Entities.De", "De")
                        .WithMany("SoanDes")
                        .HasForeignKey("DeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyQuizApi.Model.Entities.GiaoVien", "GiaoVien")
                        .WithMany("SoanDes")
                        .HasForeignKey("GiaoVienId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
