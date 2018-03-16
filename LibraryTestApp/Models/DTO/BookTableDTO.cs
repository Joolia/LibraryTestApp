using System.Collections.Generic;

namespace LibraryTestApp.Models.DTO
{
    public class BookTableDTO
    {
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public int recordsFiltered { get; set; }
        public List<BookTableItem> aaData { get; set; }
    }
}