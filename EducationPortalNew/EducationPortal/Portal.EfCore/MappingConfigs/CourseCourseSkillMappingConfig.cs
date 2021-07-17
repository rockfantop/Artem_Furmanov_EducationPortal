using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.MappingConfigs
{
    public class CourseCourseSkillMappingConfig : IEntityTypeConfiguration<CourseCourseSkill>
    {
        public void Configure(EntityTypeBuilder<CourseCourseSkill> builder)
        {
            builder.ToTable("CourseCourseSkills", "sch");

            builder.HasKey(x => new { x.CourseId, x.CourseSkillId });

            builder.HasOne(x => x.CourseSkill)
                .WithMany(p => p.CourseCourseSkills)
                .HasForeignKey(pt => pt.CourseSkillId);

            builder.HasOne(x => x.Course)
                .WithMany(p => p.CourseCourseSkills)
                .HasForeignKey(pt => pt.CourseId);
        }
    }
}
