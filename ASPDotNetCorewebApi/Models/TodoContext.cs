using Microsoft.EntityFrameworkCore;

namespace ASPDotNetCorewebApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Developer> Developers { get; set;} = null!;
    }
}