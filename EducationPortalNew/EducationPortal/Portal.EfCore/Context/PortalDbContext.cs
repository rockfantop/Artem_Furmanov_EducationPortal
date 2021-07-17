using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using Portal.EfCore.MappingConfigs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.Context
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMappingConfig).Assembly);

            modelBuilder.Entity<TextMaterial>().ToTable("TextMaterials");
            modelBuilder.Entity<InternetMaterial>().ToTable("InternetMaterials");
            modelBuilder.Entity<VideoMaterial>().ToTable("VideoMaterials");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSkill> CourseSkills { get; set; }
        public DbSet<CourseCourseSkill> CourseCourseSkill { get; set; }
        public DbSet<UserCourseSkill> UserCourseSkills { get; set; }
        public DbSet<ProgressBar> ProgressBars { get; set; }
        public DbSet<UserMaterial> UserMaterials { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<TextMaterial> TextMaterials { get; set; }
        public DbSet<InternetMaterial> InternetMaterials { get; set; }
        public DbSet<VideoMaterial> VideoMaterials { get; set; }
    }
}
