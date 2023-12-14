using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookBlogApp.Models
{
    public class BookDataModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Movie Img")]
        [NotMapped]
        public IFormFile MovieImg { get; set; }
    }
}