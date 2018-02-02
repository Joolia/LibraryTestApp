using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using LibraryTestApp.Models;

namespace LibraryTestApp.EntityClasses
{
    public class BooksCatalogue : DbContext
    {
        // Your context has been configured to use a 'BooksCatalogue' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryTestApp.Models.BooksCatalogue' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BooksCatalogue' 
        // connection string in the application configuration file.
        public BooksCatalogue()
            : base("name=BooksCatalogue")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
    }

    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
        }
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime PublishingDate { get; set; }
        [Required]
        public virtual ICollection<Author> Authors { get; set; }

        [Range(1, 10, ErrorMessage = "Rating must be in range from 1 to 10.")]
        public int Rating { get; set; }

        public int PagesCount { get; set; }
    }

    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}