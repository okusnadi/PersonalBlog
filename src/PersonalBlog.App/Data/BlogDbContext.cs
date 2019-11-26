using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PersonalBlog.App.Data
{
    public class BlogDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Data/Blog.db")
                ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var post = modelBuilder.Entity<Post>();

            post.HasKey(m => m.Id);
            post.Property(m => m.Id).HasMaxLength(256);
            post.Property(m => m.Title).IsRequired().HasMaxLength(128);
            post.Property(m => m.Content).IsRequired();
            post.Property(m => m.IsTop);
            post.Property(m => m.CreatedTime).ValueGeneratedOnAdd();
            post.HasOne(m => m.Category).WithMany(m => m.Posts).HasForeignKey(m=>m.CategoryId);
            post.ToTable("Posts");


            var category = modelBuilder.Entity<PostCategory>();

            category.HasKey(m => m.Id);
            category.Property(m => m.Id).ValueGeneratedOnAdd();
            category.Property(m => m.Name).IsRequired().HasMaxLength(60);
            category.Property(m => m.SortNo);
            category.ToTable("Categories");
        }
    }
}
