using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.MappingConfigs
{
    public class CourseSkillMappingConfig : IEntityTypeConfiguration<CourseSkill>
    {
        public void Configure(EntityTypeBuilder<CourseSkill> builder)
        {
            builder.ToTable("CourseSkills", "sch");

            builder.HasKey(x => x.Id)
                .IsClustered();

            builder.Property(x => x.Title)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(x => x.Description)
                .HasMaxLength(128);
        }
    }
}
