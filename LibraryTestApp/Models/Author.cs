using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryTestApp.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "First name isn't specified.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name isn't specified.")]
        public string LastName { get; set; }
        //public int BooksCount { get; set; }
        public List<int> Books { get; set; }
    }
}