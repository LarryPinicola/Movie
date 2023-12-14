using BookBlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBlogApp.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BookDataModel> Blogs { get; set; }
    }
}
