using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.MappingConfigs
{
    public class MaterialMappingConfig : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials", "dbo");

            builder.HasKey(x => x.Id)
                .IsClustered();

            builder.Property(x => x.Title)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(x => x.Author)
                .HasMaxLength(128);

            builder.Property(x => x.DateOfPublication)
                .IsRequired();

            builder.Property(x => x.Content)
                .IsRequired();

            builder.HasOne(x => x.Course)
                .WithMany(x => x.Materials)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
