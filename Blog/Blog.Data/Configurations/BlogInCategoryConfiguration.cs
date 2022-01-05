using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Configurations
{
    public class BlogInCategoryConfiguration : IEntityTypeConfiguration<BlogInCategory>
    {
        public void Configure(EntityTypeBuilder<BlogInCategory> builder)
        {
            builder.HasKey(t => new {t.CategoryId, t.BlogId });

            builder.ToTable("BlogInCategories");

            builder.HasOne(t => t.Blog).WithMany(pc => pc.BlogInCategories)
                .HasForeignKey(pc=> pc.BlogId);

            builder.HasOne(t => t.Category).WithMany(pc => pc.BlogInCategories)
              .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
