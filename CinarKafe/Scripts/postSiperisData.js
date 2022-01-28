$("body").on("click", "#siperisOnaylaButton", function () {

    var orders = new Array();

    var masaId = $("#item_MasaId :selected").attr('value');

    $("#siperisOnaylaButton").prop('disabled', true);

    $("#orderListTable TBODY TR").each(function () {
        var row = $(this);
        var order = {};

        order.UrunId = row.find("TD").eq(0).html();
        order.UrunKategorisiId = row.find("TD").eq(1).html();
        order.UrunAdi = row.find("TD").eq(2).html();
        order.UrunKategorisiAdi = row.find("TD").eq(3).html();
        order.Fiyat = row.find("TD").eq(4).html();
        order.StokMiktari = row.find("TD").eq(5).html();
        order.Adet = row.find("TD").eq(6).html();
        order.ToplamFiyat = row.find("TD").eq(7).html();
        order.Tarihi = row.find("TD").eq(8).html();
        order.MasaId = row.find("TD").eq(9).html();

        orders.push(order);
    });

    $(".spinner-border").show();

    //Send the JSON array to Controller using AJAX.
    $.ajax({
        url: "/api/InsertOrders/",
        method: "POST",
        data: JSON.stringify({ "UrunListesi": orders, "MasaId": masaId }),
        contentType: "application/json; charset=utf-8",
        success: function () {
            // clear cart 
            localStorage.clear();

            // show success message
            toastr.success("Siperişinizi verildi");

            location.reload();

            $(".spinner-border").hide();

            // enable button 
            $("#siperisOnaylaButton").prop('disabled', false);
        },
        error: function () {
            toastr.error("Server'da bir hata oluştu");
        }
    });
}); 
