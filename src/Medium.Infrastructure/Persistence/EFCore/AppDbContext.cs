using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medium.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medium.Infrastructure.Persistence.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Blog.Entities.Blog> Blogs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<MediaObject> MediaObjects { get; set; }
    }
}
