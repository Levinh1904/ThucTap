using Blog.Data.Configurations;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.EF
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
           

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Attachment> Attachments { get; set; }


        public DbSet<Blogss> Blogsss { get; set; }

        public DbSet<Command> Commands { get; set; }
        public DbSet<CommandInFunction> CommandInFunctions { get; set; }

        public DbSet<ErrorViewModel> ErrorViewModels { get; set; }

        public DbSet<Function> Functions { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }

        public DbSet<User> Users { get; set; }


        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }



    }
}
