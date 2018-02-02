//$(function () {

//   function initInputs() {
//      $("#authorsSelect").chosen({
//         no_results_text: "No authors found",
//         });
//      $("#bookRating").chosen({
//         disable_search: true,
//         width: 169
//      });
//      $('#PublishingDate').datepicker({
//         dateFormat: "dd.mm.yy",
//         changeMonth: true,
//         changeYear: true,
//         yearRange: "-60:+0",
//         defaultDate: 1
//      });

//      $("#PublishingDate").val("Select date...");
//      $("#PagesCount").val("");

//      $("#saveBookBtn").click(function () {
//         var $form = $(this).parents('form');
//         var model = $form.serialize();

//         $.ajax({
//            type: "POST",
//            url: $form.attr('action'),
//            data: $form.serialize(),
//            error: function (xhr, status, error) {
//            },
//            success: function (response) {
//               if (response.success) {
//                  $("#booksTable").DataTable().destroy();
//                  initDataTable("#booksTable").ajax.reload();
//                  alert(response.message);
//               }
//            }
//         });
//      });
//   }

//   initInputs();

//});


