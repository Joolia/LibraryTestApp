using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace LibraryTestApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime PublishingDate { get; set; }
        [Required]
        public List<int> Authors { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Rating must be in range from 1 to 10.")]
        public int Rating { get; set; }

        [Required]
        public int PagesCount { get; set; }
    }
}