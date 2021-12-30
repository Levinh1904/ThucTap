using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // do AppRole đã có id do class IdentityUser chỉ định nên không cần
            // xác định id cho Entity này

            builder.ToTable("AppRoles");

            builder.Property(x => x.Id).HasMaxLength(200);
        }
    }
}
