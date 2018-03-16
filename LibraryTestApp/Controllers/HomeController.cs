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
            bService.AddBook(book);
            return Json(new {success = "True", message = $"The book {book.Name} was created successfully."},
                JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult TableAjaxHandler(jQueryDataTableParamModel param)
        {
            var tableData = bService.GetTableData(param);
            return Json(
                new
                {
                    iTotalRecords = tableData.iTotalRecords,
                    iTotalDisplayRecords = tableData.iTotalDisplayRecords,
                    recordsFiltered = tableData.recordsFiltered,
                    aaData = tableData.aaData    
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
                bService.UpdateBook(book);
                return Json(new {success = "True", message = $"The book {book.Name} was updated successfully."}
                    , JsonRequestBehavior.AllowGet);
            }

            //return Json(new { success = "True", message = $"The book {book.Name} was created successfully." });
            return AddBookAjax(book);
        }

        public JsonResult DeleteBook(int id)
        {
            bService.RemoveBook(id);
            return Json(new {success = "True", message = $"The book was deleted."},
                JsonRequestBehavior.AllowGet);
        }
    }
}