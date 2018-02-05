$(function () {
   function initDataTable(tableId) {
      return $(tableId).DataTable({
         "serverSide": true,
         "ajax": "Home/TableAjaxHandler",
         "processing": true,
         "lengthChange": false,
         "displayLength": 10, 
         //"bInfo": false,    
         //"ordering": false,
         "initComplete": function () {
            $(".delete_link").click(function () {
               if (confirm("Do you want to delete the book?")) {
                  var id = $(this).parent().data("bookid");
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
               var id = $(this).parent().data("bookid");
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
         "columns": [
            { "data": "Id" },
            {
               "data": "Name",
               "render": function(data, type, row) {
                  return '<div data-bookid=\"' + row.Id + '\">' +
                     '<a href=\"javascript:void(0)\" class=\"delete_link\"><i class=\"fa fa-trash\"></i></a> ' +
                     '<a href=\"javascript:void(0)\" class=\"book_link\">' + data + '</a>' +
                     '</div>';
               }
            },
            { "data": "PagesCount" },
            {
               "data": "Authors",
               "render": function (data, type, row) {
                  var namesStr = data.map(function(item) {
                     return '<a href=\"javascript:void(0)\" class=\"author_link\" data-authorid=\"' + item.Id + '\">' + item.FirstName + " " + item.LastName + '</a>';
                  });

                  return namesStr.join(", ");
               }
            },
            { "data": "PublishsingDateFormatted" },
            { "data": "Rating" }
         ]
      });
   }
   initDataTable("#booksTable");
   });

