$(function () {
   initDataTable("#booksTable");
   bindBookTableEvents();  
});
var initDataTable = function (tableId) {
   return $(tableId).DataTable({
      "serverSide": true,
      "ajax": "Home/TableAjaxHandler",
      "processing": true,
      "lengthChange": false,
      "displayLength": 10,
      //"bInfo": false,    
      //"ordering": false,
      "initComplete": function () {
      },
      "columns": [
         { "data": "Id" },
         {
            "data": "Name",
            "render": function (data, type, row) {
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
               var namesStr = data.map(function (item) {
                   return '<a href=\"' + item.FirstName + "_" + item.LastName + '\" class=\"author_link\" data-authorid=\"' + item.Id + '\">' + item.FirstName + " " + item.LastName + '</a>';
               });

               return namesStr.join(", ");
            }
         },
         { "data": "PublishsingDateFormatted" },
         { "data": "Rating" }
      ]
   });
}
//var editBookAjax = function (form) {
//   $.ajax({
//      type: "POST",
//      url: form.attr('action'),
//      data: form.serialize(),  // add unobtrusive validation || ajax.beginform
//      error: function (xhr, status, error) {
//      },
//      success: function (response) {
//         if (response.success) {
//            onEditCreateSuccess();
//         }
//      }
//   });
//}

var bindBookTableEvents = function(){
   $("#booksTable").on("click", ".delete_link", function () {
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
   $("#booksTable").on("click", ".book_link", function () {
      var id = $(this).parent().data("bookid");
      $.ajax({
         type: "GET",
         url: 'Home/AddBookPartial',
         data: { id: id },
         error: function (xhr, status, error) {
         },
         success: function (response) {
            $("#addBookContainer").html(response);
            $("html, body").animate({
               scrollTop: 0
            }, 200);
            //initInputs();
         }
      });
   });
}

var onEditCreateSuccess = function(response) {
   $("#booksTable").DataTable().destroy();
   initDataTable("#booksTable").ajax.reload(function () {   
      alert(response.message);
      editBookPartialAjax();
   });
}  
var initInputs = function () {
   $("#authorsSelect").chosen({
      no_results_text: "No authors found",
   });
   $("#bookRating").chosen({
      disable_search: true,
      width: 169
   });
   $('#PublishingDate').datepicker({
      dateFormat: "dd.mm.yy",
      changeMonth: true,
      changeYear: true,
      yearRange: "-60:+0"
   });

   //$("#saveBookBtn").click(function () {
   //   var $form = $(this).parents('form');
   //   var model = $form.serialize();

   //   editBookAjax($form);
   //});
}
var editBookPartialAjax = function() {
   $.ajax({
      url: "Home/AddBookPartial",
      success: function(html) {
         $("#addBookContainer").html(html);
      }
   });
}

