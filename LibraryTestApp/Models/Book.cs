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
        [Required(ErrorMessage = "Name isn't filled.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Publishing date isn't selected.")]
        public DateTime PublishingDate { get; set; }
        [Required(ErrorMessage = "Authors aren't specified.")]
        public List<int> Authors { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Rating must be in range from 1 to 10.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "The number of pages isn't specified.")]
        public int PagesCount { get; set; }
    }
}