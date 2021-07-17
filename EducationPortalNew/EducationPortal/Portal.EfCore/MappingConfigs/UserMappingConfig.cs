using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.MappingConfigs
{
    public class UserMappingConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "sch");

            builder.HasKey(x => x.Id)
                .IsClustered();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(x => x.Email)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(70)
                .IsRequired();
        }
    }
}
