using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    [Table("Tbl_Book")]
    public class BookDataModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        [Column("Genre")]
        public string? Genre { get; set; }
        public int PublishedDate { get; set; }
        public string? ImageUrl { get; set; }

        [Display(Name = "Movie Img")]
        [NotMapped]
        public IFormFile? BookImg { get; set; }
    }
}
