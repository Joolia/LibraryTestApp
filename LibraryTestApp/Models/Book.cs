using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryTestApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishingDate { get; set; }
        public List<int> Authors { get; set; }

        [Range(1, 10, ErrorMessage = "Rating must be in range from 1 to 10.")]
        public int Rating { get; set; }

        public int PagesCount { get; set; }
    }
}