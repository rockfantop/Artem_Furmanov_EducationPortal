﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portal.EfCore.Context;

namespace Portal.EfCore.Migrations
{
    [DbContext(typeof(PortalDbContext))]
    [Migration("20210622231657_ModelAndRelationshipsCreated")]
    partial class ModelAndRelationshipsCreated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Portal.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .IsClustered();

                    b.HasIndex("OwnerId");

                    b.ToTable("Courses", "sch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseCourseSkill", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseSkillId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseId", "CourseSkillId");

                    b.HasIndex("CourseSkillId");

                    b.ToTable("CourseCourseSkills", "sch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .IsClustered();

                    b.ToTable("CourseSkills", "sch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfPublication")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .IsClustered();

                    b.HasIndex("CourseId");

                    b.ToTable("Materials", "sch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.ProgressBar", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProgressBars", "sch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id")
                        .IsClustered();

                    b.ToTable("Users", "sch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.UserCourseSkill", b =>
                {
                    b.Property<Guid>("CourseSkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SkillLevel")
                        .HasColumnType("int");

                    b.HasKey("CourseSkillId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCourseSkills", "sch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.UserMaterial", b =>
                {
                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPassed")
                        .HasColumnType("bit");

                    b.HasKey("MaterialId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMaterials", "sch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.InternetMaterial", b =>
                {
                    b.HasBaseType("Portal.Domain.Entities.Material");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("InternetMaterials");
                });

            modelBuilder.Entity("Portal.Domain.Entities.TextMaterial", b =>
                {
                    b.HasBaseType("Portal.Domain.Entities.Material");

                    b.Property<string>("Format")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.ToTable("TextMaterials");
                });

            modelBuilder.Entity("Portal.Domain.Entities.VideoMaterial", b =>
                {
                    b.HasBaseType("Portal.Domain.Entities.Material");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("Quality")
                        .HasColumnType("int");

                    b.ToTable("VideoMaterials");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Course", b =>
                {
                    b.HasOne("Portal.Domain.Entities.User", "Owner")
                        .WithMany("OwnedCourses")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseCourseSkill", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Course", "Course")
                        .WithMany("CourseCourseSkills")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portal.Domain.Entities.CourseSkill", "CourseSkill")
                        .WithMany("CourseCourseSkills")
                        .HasForeignKey("CourseSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("CourseSkill");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Material", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Course", "Course")
                        .WithMany("Materials")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Portal.Domain.Entities.ProgressBar", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Course", "Course")
                        .WithMany("ProgressBars")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portal.Domain.Entities.User", "User")
                        .WithMany("SubscribedCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Portal.Domain.Entities.UserCourseSkill", b =>
                {
                    b.HasOne("Portal.Domain.Entities.CourseSkill", "CourseSkill")
                        .WithMany("UserCourseSkills")
                        .HasForeignKey("CourseSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portal.Domain.Entities.User", "User")
                        .WithMany("UserCourseSkills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseSkill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Portal.Domain.Entities.UserMaterial", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Material", "Material")
                        .WithMany("UserMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portal.Domain.Entities.User", "User")
                        .WithMany("UserMaterials")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Portal.Domain.Entities.InternetMaterial", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Material", null)
                        .WithOne()
                        .HasForeignKey("Portal.Domain.Entities.InternetMaterial", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Portal.Domain.Entities.TextMaterial", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Material", null)
                        .WithOne()
                        .HasForeignKey("Portal.Domain.Entities.TextMaterial", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Portal.Domain.Entities.VideoMaterial", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Material", null)
                        .WithOne()
                        .HasForeignKey("Portal.Domain.Entities.VideoMaterial", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Portal.Domain.Entities.Course", b =>
                {
                    b.Navigation("CourseCourseSkills");

                    b.Navigation("Materials");

                    b.Navigation("ProgressBars");
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseSkill", b =>
                {
                    b.Navigation("CourseCourseSkills");

                    b.Navigation("UserCourseSkills");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Material", b =>
                {
                    b.Navigation("UserMaterials");
                });

            modelBuilder.Entity("Portal.Domain.Entities.User", b =>
                {
                    b.Navigation("OwnedCourses");

                    b.Navigation("SubscribedCourses");

                    b.Navigation("UserCourseSkills");

                    b.Navigation("UserMaterials");
                });
#pragma warning restore 612, 618
        }
    }
}
