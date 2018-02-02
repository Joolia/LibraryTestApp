using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;

namespace LibraryTestApp.Models
{
    public class BookTableItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime PublishingDate { get; set; }
       
        public  List<EntityClasses.Author> Authors { get; set; }

        public int Rating { get; set; }

        public int PagesCount { get; set; }

        // to datatable 
        public string EditLink => $"<a href=\"javascript:void(0)\" class=\"delete_link\" data-bookid=\"{Id}\"><i class=\"fa fa-trash\"></i></a>  <a href=\"javascript:void(0)\" class=\"book_link\" data-bookid=\"{Id}\">{Name}</a>";

        public string AuthorsListLinks => string.Join(",",
            Authors.Select(a =>
                $"<a href=\"{ToUnderscore(a.FirstName)}/{ToUnderscore(a.LastName)}\">{a.FirstName} {a.LastName}<a/>"));

        //public string ToAuthorLink(string urlActionLink, Models.Author a)
        //{
        //    return Authors.Select(a => )
        //    //return $"<a href=\"{urlActionLink}\">{a.FirstName} {a.LastName}<a/>";
        //}

        private string ToUnderscore(string s)
        {
            return s.Replace(" ", "_");
        }
    }
}