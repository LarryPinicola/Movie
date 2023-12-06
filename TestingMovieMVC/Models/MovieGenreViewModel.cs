using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestingMovieMVC.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie>? Movies { get; set; }

        public SelectList? Genres { get; set; } // contain list of genres, allows user to select genre

        public string? MovieGenre { get; set; } //which contains the selected genre

        public string? SearchString { get; set; } // contains text users enter in searchBox
    }
}
