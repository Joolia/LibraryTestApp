using System.Web.Mvc;
using LibraryTestApp.DbServices;
using LibraryTestApp.Models;

namespace LibraryTestApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BooksService bService = new BooksService();
        private readonly AuthorsService aService = new AuthorsService();

        // GET: Author
        public ActionResult EditAuthor(string fullname)
        {
            var authorNames = fullname.Split('_');

            var author = aService.GetAuthor(authorNames[0], authorNames[1]);
            return View("EditAuthor", author);
        }

        [HttpPost]
        public ActionResult EditAuthorPost(Author author)
        {
            aService.EditAuthor(author.Id, author.FirstName, author.LastName);
            return RedirectToAction("Index","Home");

        }
    }
}