$(function () {
   function initDataTable(tableId) {
      return $(tableId).DataTable({
         "bServerSide": true,
         "sAjaxSource": "Home/TableAjaxHandler",
         "bProcessing": true,
         "bLengthChange": false,
         "bInfo": false,
         "ordering": false,
         "initComplete": function () {
            $(".delete_link").click(function () {
               if (confirm("Do you want to delete the book?")) {
                  var id = $(this).data("bookid");
                  $.ajax({
                     type: "POST",
                     url: 'Home/DeleteBook',
                     data: { id: id },
                     error: function (xhr, status, error) {
                     },
                     success: function (response) {
                        $("#booksTable").DataTable().destroy();
                        initDataTable("#booksTable").ajax.reload();
                        //alert(response.message);
                     }
                  });
               } else {
                  return false;
               }
            });
            $(".book_link").click(function () {
               var id = $(this).data("bookid");
               $.ajax({
                  type: "GET",
                  url: 'Home/AddBookPartial',
                  data: { id: id },
                  error: function (xhr, status, error) {
                  },
                  success: function (response) {
                     $("#addBookContainer").html(response);
                     //initInputs();
                  }
               });
            });
         },
         "aoColumns": [
            {
               "sName": "ID",
               "bSearchable": false,
               "bSortable": false
            },
            { "sName": "BOOK_NAME" },
            { "sName": "PAGES" },
            { "sName": "AUTHORS" },
            { "sName": "PUBLISHED" },
            { "sName": "RATING" }
         ]
      });
   }
   initDataTable("#booksTable");
   });

