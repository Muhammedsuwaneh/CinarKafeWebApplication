$(document).ready(function () {
      var table = $("#masaTable");

       table.on("click", ".js-delete", function () {
            var button = $(this);
            bootbox.confirm("Bu masayi silmek istediğinizden emin misiniz ?",
                function (result) {
                    if (result) {
                        $.ajax({
                            url: "/api/DeleteMasa/" + button.attr("data-masa-id"),
                            method: "DELETE",
                            success: function () {
                                toastr.success("Masa silindi");
                                table.remove();
                            },
                            error: function (xhr, status, error) {
                                var err = eval("(" + xhr.responseText + ")");
                                toastr.error(err.Message);
                            }
                        });
                    }
                }
            );
        });
});