using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LibraryTestApp.DbServices;
using LibraryTestApp.EntityClasses;
using LibraryTestApp.Models;

namespace LibraryTestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BooksService bService = new BooksService();
        private readonly AuthorsService aService = new AuthorsService();

        public ActionResult Index()
        {
            return View("Index");
        }

        public JsonResult AddBookAjax(Models.Book book)
        {
            return Json(
                bService.AddBook(book) > 0
                    ? new {success = "True", message = $"The book {book.Name} was created successfully."}
                    : new {success = "False", message = $"The book {book.Name} already exists."},
                JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult TableAjaxHandler(jQueryDataTableParamModel param)
        {
            var totalBooks = bService.GetBookTableItems();
            var booksCount = totalBooks.Count;
            var filteredBooks = !string.IsNullOrWhiteSpace(param.sSearch)
                ? totalBooks.Where(b => b.Filter(param.sSearch))
                : totalBooks;
            var booksToDisplay = filteredBooks;
            var result = from book in booksToDisplay
                select new BookTableItem
                {
                    Id = book.Id,
                    Name = book.Name,
                    //book.EditLink,
                    PagesCount = book.PagesCount,
                    //book.AuthorsListLinks,
                    Authors = book.Authors,
                    PublishingDate = book.PublishingDate.Date,
                    Rating = book.Rating
                };
            var filterdCount = filteredBooks.Count();
            return Json(
                new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = booksCount,
                    iTotalDisplayRecords = filterdCount,
                    recordsFiltered = filterdCount,
                    aaData = result.ToList()    
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddBookPartial(int id = 0)
        {
            var authors = aService.GetAuthors();
            ViewBag.Authors = authors;
            var model = bService.GetBook(id);
            return View("AddBookPartial", model);
        }

        [HttpPost]
        public JsonResult EditCreateBook(Models.Book book)
        {
            if (book.Id != 0)
            {
                return Json(
                    bService.UpdateBook(book) > 0
                        ? new {success = "True", message = $"The book {book.Name} was updated successfully."}
                        : new {success = "False", message = $"Something went wrong."}, JsonRequestBehavior.AllowGet);
            }

            //return Json(new { success = "True", message = $"The book {book.Name} was created successfully." });
            return AddBookAjax(book);
        }

        public JsonResult DeleteBook(int id)
        {
            return Json(
                bService.RemoveBook(id) > 0
                    ? new {success = "True", message = $"The book was deleted."}
                    : new {success = "False", message = $"Something went wrong."}, JsonRequestBehavior.AllowGet);
        }
    }
}