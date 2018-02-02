using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryTestApp.EntityClasses;
using LibraryTestApp.Models;

namespace LibraryTestApp.DbServices
{
    public class AuthorsService
    {
        public List<Models.Author> GetAuthors()
        {
            using (var ctx = new BooksCatalogue())
            {
                return ctx.Authors.Select(a => new Models.Author
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Books = a.Books.Select(b => b.Id).ToList()
                }).ToList();
            }
        }

        public Models.Author GetAuthor(string fName, string lName)
        {
            using (var ctx = new BooksCatalogue())
            {
                var author = ctx.Authors.Select(a => new Models.Author
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Books = a.Books.Select(b => b.Id).ToList()
                }).FirstOrDefault(a => a.FirstName == fName && a.LastName == lName);
                //if (author != null)
                //{
                //    author.Books = ctx.Books.Where(b => b.Authors.Contains(author)).Select(a => new Models.Author
                //    {
                //        FirstName = a.
                //    }).ToList();
                //}
                return author;
            }
        }
        
        public int AddAuthor(Models.Author author)
        {
            using (var ctx = new BooksCatalogue())
            {
                if (!ctx.Authors.Any(a => a.FirstName == author.FirstName && a.LastName == author.LastName))
                {
                    ctx.Authors.Add(new EntityClasses.Author {FirstName = author.FirstName, LastName = author.LastName });
                    return ctx.SaveChanges();
                }
                else
                {
                    return 0;
                }
            }
        }

        public int EditAuthor(int authorId, string newFname, string newLname)
        {
            using (var ctx = new BooksCatalogue())
            {
                var author = ctx.Authors.FirstOrDefault(a => a.Id == authorId);

                if (author != null)
                {
                    author.FirstName = newFname;
                    author.LastName = newLname;
                    return ctx.SaveChanges();
                }
                else
                {
                    return 0;
                }
              }
        }
    }
}