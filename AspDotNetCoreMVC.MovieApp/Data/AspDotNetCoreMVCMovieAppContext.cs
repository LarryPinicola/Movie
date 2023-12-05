using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspDotNetCoreMVC.MovieApp.Models;

namespace AspDotNetCoreMVC.MovieApp.Data
{
    public class AspDotNetCoreMVCMovieAppContext : DbContext
    {
        public AspDotNetCoreMVCMovieAppContext (DbContextOptions<AspDotNetCoreMVCMovieAppContext> options)
            : base(options)
        {
        }

        public DbSet<AspDotNetCoreMVC.MovieApp.Models.Movie> Movie { get; set; } = default!;
    }
}
