using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        [DataType(DataType.Date)] public DateTime ReleaseDate { get; set; }

        public string? Genre { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        //[Required(ErrorMessage ="Please choose Movie image")]
        [Display(Name ="Movie Img")]
        [NotMapped]
        public IFormFile? MovieImg { get; set; }
    }
}
