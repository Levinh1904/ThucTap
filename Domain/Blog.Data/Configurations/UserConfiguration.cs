using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // do AppUser đã có id do class IdentityUser chỉ định nên không cần
            // xác định id cho Entity này

            builder.ToTable("Users");
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(200);
        }
    }
}
