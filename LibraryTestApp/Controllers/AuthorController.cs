using System.Web.Mvc;
using LibraryTestApp.DbServices;

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
        public ActionResult EditAuthorPost(int id, string firstName, string lastName)
        {
            aService.EditAuthor(id, firstName, lastName);
           //return Json(aService.EditAuthor(id, firstName, lastName) > 0 ? new { success = "True", message = $"Author {firstName} {lastName} was edited successfully." }
           //        : new { success = "False", message = $"Something went wrong." },
           //    JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index","Home");

        }
    }
}