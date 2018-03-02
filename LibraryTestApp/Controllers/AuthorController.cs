using System.Web.Mvc;
using LibraryTestApp.DbServices;

namespace LibraryTestApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BooksService bService = new BooksService();
        private readonly AuthorsService aService = new AuthorsService();

        // GET: Author
        public ActionResult EditAuthor(string firstName, string lastName)
        {
            var author = aService.GetAuthor(firstName, lastName);
            return View("EditAuthor", author);
        }

        public JsonResult EditAuthorPost(int id, string firstName, string lastName)
        {
           return Json(aService.EditAuthor(id, firstName, lastName) > 0 ? new { success = "True", message = $"Author {firstName} {lastName} was edited successfully." }
                   : new { success = "False", message = $"Something went wrong." },
               JsonRequestBehavior.AllowGet);
        }
    }
}