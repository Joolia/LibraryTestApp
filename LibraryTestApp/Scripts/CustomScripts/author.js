$(function () {
   $("#saveAuthorBtn").click(function () {
      var $form = $(this).parents('form');
      var model = $form.serialize();
      var authorId = $("#editAuthorForm").data("authorid");
      var newFname = $("#FirstName").val();
      var newLname = $("#LastName").val();

      $.ajax({
         type: "POST",
         url: $form.attr('action'),
         data: { id: authorId, firstName: newFname, lastName: newLname },
         error: function (xhr, status, error) {
         },
         success: function (response) {
            if (response.success) {
               alert(response.message);
            }
         }
      });
   });
});