$(document).ready(function () {
    var table = $("#masaBilgileri");

    $(".spinner-border").hide();

    table.on("click", ".js-odeme", function () {
        var button = $(this);

        bootbox.confirm("Bu urun için ödeme onaylamak ister misiniz ?",
            function (result) {
                if (result) {
                    $(".spinner-border").show();
                    button.prop('disabled', true);
                    $.ajax({
                        url: "/api/MakeSiperisPayment/" + detailId,
                        method: "PUT",
                        dataSrc: "",
                        success: function () {
                            $(".spinner-border").hide();
                            toastr.success("Ödeme gerçekleştirildi");
                            table.remove();
                        },
                        error: function (xhr, status, error) {
                            $(".spinner-border").hide();
                            var err = eval("(" + xhr.responseText + ")");
                            toastr.error(err.Message);
                        }
                    });
                }
            }
        );
    });
});