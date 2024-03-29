﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingMovieMVC.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseDate { get; set; }

        public string? Genre { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string? Rating { get; set; }

        [Display(Name ="ImageName")]
        [NotMapped]
        public IFormFile imgFile { get; set; }
    }
}
