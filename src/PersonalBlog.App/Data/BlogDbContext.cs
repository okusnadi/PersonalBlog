using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PersonalBlog.App.Data
{
    public class BlogDbContext : DbContext
    {
        private readonly IWebHostEnvironment _env;

        public BlogDbContext(IWebHostEnvironment env) : base()
        {
            this._env = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_env.ContentRootPath}/Data/Blog.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity= modelBuilder.Entity<Post>();

            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).HasMaxLength(256);
            entity.Property(m => m.Title).IsRequired().HasMaxLength(128);
            entity.Property(m => m.Content).IsRequired();
            entity.Property(m => m.IsTop);
            entity.Property(m => m.CreatedTime).ValueGeneratedOnAdd();
            entity.Property(m => m.Category).IsRequired().HasMaxLength(100);
            entity.ToTable("Posts");
        }
    }
}
