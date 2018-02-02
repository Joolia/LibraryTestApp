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

        // to service
        public ActionResult TableAjaxHandler(jQueryDataTableParamModel param)
        {
            using (var ctx = new BooksCatalogue())
            {
                var totalBooks = ctx.Books.Select(b=> new Models.BookTableItem
                {
                    Id = b.Id,
                    Name = b.Name,
                    Authors = b.Authors.ToList(),
                    PagesCount = b.PagesCount,
                    PublishingDate = b.PublishingDate,
                    Rating = b.Rating
                }).OrderByDescending(x => x.Id).ToList();

                var au = ctx.Books.Select(x => x.Authors);
                var booksCount = totalBooks.Count;
                var filteredBooks = !string.IsNullOrWhiteSpace(param.sSearch)
                    ? totalBooks.Where(b => Filter(b, param.sSearch))
                    : totalBooks;
                var booksToDisplay = filteredBooks.Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength);
                var result = from book in booksToDisplay
                    select new[]
                    {
                        book.Id.ToString(),
                        book.EditLink,
                        book.PagesCount.ToString(),
                        book.AuthorsListLinks,
                        book.PublishingDate.ToShortDateString(), book.Rating.ToString()
                    };
                var filterdCount = filteredBooks.Count();
                return Json(
                    new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = booksCount,
                        iTotalDisplayRecords = filterdCount,
                        recordsFiltered = filterdCount,
                        aaData = result
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddBookPartial(int id=0)
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
                        : new {success = "False", message = $"Something went wrong."},
                    JsonRequestBehavior.AllowGet);
            }

            //return Json(new { success = "True", message = $"The book {book.Name} was created successfully." });
            return AddBookAjax(book);
        }

        public JsonResult DeleteBook(int id)
        {
            return Json(
                bService.RemoveBook(id) > 0
                    ? new { success = "True", message = $"The book was deleted." }
                    : new { success = "False", message = $"Something went wrong." },
                JsonRequestBehavior.AllowGet);
        }

        #region private methods

        private bool Filter(Models.BookTableItem book, string searchText)
        {
            return book.Id.ToString().Contains(searchText) || book.Name.Contains(searchText) ||
                   book.PublishingDate.ToShortDateString().Contains(searchText) ||
                   string.Join(", ", book.Authors).Contains(searchText);
        }

        #endregion
    }
}