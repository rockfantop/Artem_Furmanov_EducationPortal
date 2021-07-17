using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.MappingConfigs
{
    public class CourseMappingConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses", "sch");

            builder.HasKey(x => x.Id)
                .IsClustered();

            builder.Property(x => x.Title)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(x => x.Description)
                .HasMaxLength(128);

            builder.Property(x => x.IsPublic);

            builder.HasOne(x => x.Owner)
                .WithMany(x => x.OwnedCourses)
                .HasForeignKey(x => x.OwnerId);
        }
    }
}
