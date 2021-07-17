using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.MappingConfigs
{
    public class UserCourseSkillMappingConfig : IEntityTypeConfiguration<UserCourseSkill>
    {
        public void Configure(EntityTypeBuilder<UserCourseSkill> builder)
        {
            builder.ToTable("UserCourseSkills", "sch");

            builder.HasKey(x => new { x.CourseSkillId, x.UserId });

            builder.HasOne(x => x.User)
                .WithMany(p => p.UserCourseSkills)
                .HasForeignKey(pt => pt.UserId);

            builder.HasOne(x => x.CourseSkill)
                .WithMany(p => p.UserCourseSkills)
                .HasForeignKey(pt => pt.CourseSkillId);
        }
    }
}
