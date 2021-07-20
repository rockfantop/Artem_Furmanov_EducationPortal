using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.MappingConfigs
{
    public class ProgressBarMappingConfig : IEntityTypeConfiguration<ProgressBar>
    {
        public void Configure(EntityTypeBuilder<ProgressBar> builder)
        {
            builder.ToTable("ProgressBars", "dbo");

            builder.HasKey(x => new { x.CourseId, x.UserId });

            builder.HasOne(x => x.User)
                .WithMany(p => p.SubscribedCourses)
                .HasForeignKey(pt => pt.UserId);

            builder.HasOne(x => x.Course)
                .WithMany(p => p.ProgressBars)
                .HasForeignKey(pt => pt.CourseId);
        }
    }
}
