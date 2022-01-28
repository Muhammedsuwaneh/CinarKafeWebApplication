$(document).ready(function () {
    var table = $("#urunTable");

    table.on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Bu urunu silmek istediğinizden emin misiniz ?",
            function (result) {
                if (result) {
                    $.ajax({
                        url: "/api/DeleteUrun/" + button.attr("data-urun-id"),
                        method: "DELETE",
                        success: function () {
                            toastr.success("Ürün silindi");
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