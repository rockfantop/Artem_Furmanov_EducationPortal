using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.EfCore.MappingConfigs
{
    public class UserMaterialMappingConfig : IEntityTypeConfiguration<UserMaterial>
    {
        public void Configure(EntityTypeBuilder<UserMaterial> builder)
        {
            builder.ToTable("UserMaterials", "dbo");

            builder.HasKey(x => new { x.MaterialId, x.UserId });

            builder.HasOne(x => x.User)
                .WithMany(p => p.UserMaterials)
                .HasForeignKey(pt => pt.UserId);

            builder.HasOne(x => x.Material)
                .WithMany(p => p.UserMaterials)
                .HasForeignKey(pt => pt.MaterialId);
        }
    }
}
