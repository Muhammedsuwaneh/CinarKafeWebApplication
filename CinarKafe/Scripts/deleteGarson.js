$(document).ready(function () {
       var table = $("#garsonTable");

        table.on("click", ".js-delete", function () {
            var button = $(this);
            bootbox.confirm("Bu garsonu silmek istediğinizden emin misiniz ?",
                function (result) {
                    if (result) {
                        $.ajax({
                            url: "/api/DeleteGarson/" + button.attr("data-garson-id"),
                            method: "DELETE",
                            success: function () {
                                toastr.success("Garson silindi");
                                table.remove();
                            },
                              error: function (xhr, status, error) {
                                var err = eval("(" + xhr.responseText + ")");
                                toastr.error(err.Message);
                            }
                        });
                    }
                });
        });
});