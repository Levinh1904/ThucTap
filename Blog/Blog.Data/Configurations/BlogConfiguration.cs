using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Entities.BLBlog>
    {
        public void Configure(EntityTypeBuilder<Entities.BLBlog> builder)
        {
            builder.ToTable("Blogs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);

            builder.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);


        }
    }
}
