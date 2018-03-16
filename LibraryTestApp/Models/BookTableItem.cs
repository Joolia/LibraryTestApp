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

        public string PublishsingDateFormatted => PublishingDate.ToShortDateString();

        public  List<EntityClasses.Author> Authors { get; set; }

        public int Rating { get; set; }

        public int PagesCount { get; set; }
    }
}