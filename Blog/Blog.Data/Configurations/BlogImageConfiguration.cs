using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Configurations
{
    public class BlogImageConfiguration : IEntityTypeConfiguration<BlogImage>
    {
        public void Configure(EntityTypeBuilder<BlogImage> builder)
        {
            builder.ToTable("BlogImages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.Caption).HasMaxLength(200);

            builder.HasOne(x => x.Blog).WithMany(x => x.BlogImages).HasForeignKey(x => x.BlogId);
        }
    }
}
