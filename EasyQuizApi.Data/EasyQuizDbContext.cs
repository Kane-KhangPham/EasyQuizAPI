using EasyQuizApi.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyQuizApi.Data
{
    public class EasyQuizDbContext : DbContext
    {
        public EasyQuizDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<De> Des { get; set; }
        public DbSet<DeCauHoi> DeCauHois { get; set; }
        public DbSet<GiaoVien> GiaoViens { get; set; }
        public DbSet<GiaoVienMonHoc> GiaoVienMonHocs { get; set; }
        public DbSet<KyThi> KiThis { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SoanDe> SoanDes { get; set; }

        public DbSet<Lop> Lops { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<DeCauHoi>()
                .HasKey(d => new { d.CauHoiId, d.DeId });

            modelBuilder.Entity<DeCauHoi>()
                .HasOne(d => d.CauHoi)
                .WithMany(c => c.DeCauHois)
                .HasForeignKey(d => d.CauHoiId);

            modelBuilder.Entity<DeCauHoi>()
                .HasOne(d => d.De)
                .WithMany(c => c.DeCauHois)
                .HasForeignKey(d => d.DeId);

            modelBuilder.Entity<SoanDe>()
               .HasKey(d => new { d.DeId, d.GiaoVienId });

            modelBuilder.Entity<SoanDe>()
                .HasOne(d => d.GiaoVien)
                .WithMany(c => c.SoanDes)
                .HasForeignKey(d => d.GiaoVienId);

            modelBuilder.Entity<SoanDe>()
                .HasOne(d => d.De)
                .WithMany(c => c.SoanDes)
                .HasForeignKey(d => d.DeId);

            modelBuilder.Entity<GiaoVienMonHoc>()
              .HasKey(d => new { d.MonHocId, d.GiaoVienId });

            modelBuilder.Entity<GiaoVienMonHoc>()
                .HasOne(d => d.GiaoVien)
                .WithMany(c => c.GiaoVienMonHocs)
                .HasForeignKey(d => d.GiaoVienId);

            modelBuilder.Entity<GiaoVienMonHoc>()
                .HasOne(d => d.MonHoc)
                .WithMany(c => c.GiaoVienMonHocs)
                .HasForeignKey(d => d.MonHocId);

            modelBuilder.Entity<Option>()
                .HasOne(o => o.CauHoi)
                .WithMany(q => q.Options)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<De>()
                .HasMany(x  => x.DeCauHois)
                .WithOne(d => d.De)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<De>()
                .HasMany(x  => x.SoanDes)
                .WithOne(d => d.De)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
