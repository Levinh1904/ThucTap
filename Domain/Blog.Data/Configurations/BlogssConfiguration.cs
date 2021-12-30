using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blogss>
    {
        public void Configure(EntityTypeBuilder<Blogss> builder)
        {
            builder.ToTable("Blogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.SeoAlias).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Description).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.Environment).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Problem).IsRequired().HasMaxLength(300);
            builder.Property(x => x.StepToReproduce).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Workaround).IsRequired().HasMaxLength(300);

            builder.Property(x => x.Image).HasMaxLength(300).IsRequired(false);
        }
    }
}