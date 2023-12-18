using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookBlogApp.Models
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

        /*[DataType(DataType.Date)]
        [DisplayName("Published Date")]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }*/

        public int PublishedDate { get; set; }
        public string? ImageUrl { get; set; }

        [Display(Name = "Book Img")]
        [NotMapped]
        public IFormFile? BookImg { get; set; }
    }
}