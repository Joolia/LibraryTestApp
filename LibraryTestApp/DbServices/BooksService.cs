using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using LibraryTestApp.EntityClasses;
using LibraryTestApp.Exceptions;
using LibraryTestApp.Models;

namespace LibraryTestApp.DbServices
{
    public class BooksService
    {
        
        public List<BookTableItem> GetBookTableItems()
        {
            using (var ctx = new BooksCatalogue())
            {
                try
                {
                    var totalBooks = ctx.Books.Select(b => new Models.BookTableItem
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Authors = b.Authors.ToList(),
                        PagesCount = b.PagesCount,
                        PublishingDate = b.PublishingDate,
                        Rating = b.Rating
                    }).OrderByDescending(x => x.Id).ToList();

                    return totalBooks;
                }
                catch (Exception ex)
                {
                    throw new BookException(ex.Message);
                }
            }
        }
        public void AddBook(Models.Book book)
        {
            using (var ctx = new BooksCatalogue())
            {
                try
                {
                    var authors = ctx.Authors.Where(a => book.Authors.Contains(a.Id)).ToList();
                    ctx.Books.Add(new EntityClasses.Book
                    {
                        Name = book.Name,
                        PagesCount = book.PagesCount,
                        PublishingDate = book.PublishingDate,
                        Rating = book.Rating,
                        Authors = authors
                    });

                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BookException(ex.Message);
                }
            }
        }

        public Models.Book GetBook(int id)
        {
            using (var ctx = new BooksCatalogue())
            {
                try
                {
                    return id != 0
                        ? ctx.Books.Select(b => new Models.Book
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Authors = b.Authors.Select(a => a.Id).ToList(),
                            Rating = b.Rating,
                            PagesCount = b.PagesCount,
                            PublishingDate = b.PublishingDate
                        }).FirstOrDefault(x => x.Id == id)
                        : new Models.Book();
                }
                catch (Exception ex)
                {
                    throw new BookException(ex.Message);
                }
            }
        }

        public void UpdateBook(Models.Book book)
        {
            using (var ctx = new BooksCatalogue())
            {
                try
                {
                    var dbBook = ctx.Books.Include("Authors").FirstOrDefault(b => b.Id == book.Id);
                    if (dbBook == null) throw new BookException("Book doesn't exist.");
                    var deletedAuthors = dbBook.Authors.Select(a => a.Id).Except(book.Authors).ToList();
                    var addedAuthors = book.Authors.Except(dbBook.Authors.Select(a => a.Id)).ToList();
                    deletedAuthors.ForEach(da => dbBook.Authors.Remove(dbBook.Authors.FirstOrDefault(a => a.Id == da)));

                    foreach (var author in addedAuthors)
                    {
                        var toAdd = ctx.Authors.FirstOrDefault(a => a.Id == author);
                        //dbBook.Authors.Add(toAdd);
                        if (ctx.Entry(toAdd).State == System.Data.Entity.EntityState.Detached && toAdd != null)
                            ctx.Authors.Attach(toAdd);
                        dbBook.Authors.Add(toAdd);
                    }

                    dbBook.Name = book.Name;
                    //dbBook.Authors = ctx.Authors.Where(a => book.Authors.Contains(a.Id)).ToList();
                    dbBook.PagesCount = book.PagesCount;
                    dbBook.PublishingDate = book.PublishingDate;
                    dbBook.Rating = book.Rating;

                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BookException(ex.Message);
                }

            }
        }

        public void RemoveBook(int id)
        {
            using (var ctx = new BooksCatalogue())
            {
                try
                {
                    var book = ctx.Books.FirstOrDefault(b => b.Id == id);
                    if (book == null) throw new BookException("Book doesn't exist.");
                    ctx.Books.Remove(book);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BookException(ex.Message);
                }

            }
        }
    }
}