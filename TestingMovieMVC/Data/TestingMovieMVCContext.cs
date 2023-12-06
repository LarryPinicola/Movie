using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestingMovieMVC.Models;

namespace TestingMovieMVC.Data
{
    public class TestingMovieMVCContext : DbContext
    {
        public TestingMovieMVCContext (DbContextOptions<TestingMovieMVCContext> options)
            : base(options)
        {
        }

        public DbSet<TestingMovieMVC.Models.Movie> Movie { get; set; } = default!;
    }
}
