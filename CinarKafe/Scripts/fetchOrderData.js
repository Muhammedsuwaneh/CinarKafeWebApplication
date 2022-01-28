$(document).ready(function () {
    // fetch modal order list from local storage
    var UserId = $("#currentUserId").attr('value');

    var orderKey = UserId + "-Orders";

    var orderList = JSON.parse(localStorage.getItem(orderKey));

    $(".spinner-border").hide();

    if (orderList !== null) {

        $('#orderListTable').show();
        $('#order-info').hide();
        $('#siperisOnaylaButton').show();

        var date = new Date();
        var Tarihi = date.getFullYear() + "-" + date.getMonth()+1 + "-" + date.getDay() + "-" + date.getHours() + "-" + date.getMinutes() + "-" + date.getSeconds();

        let totalPrice = 0;

        console.log(orderList);

        for (var key in orderList) {

            // populate order table with order list

            var tBody = $("#orderListTable > TBODY")[0];

            //Add Row.
            var row = tBody.insertRow(-1);

            var cell = $(row.insertCell(-1));
            cell.html(orderList[key].UrunId).hide();

            cell = $(row.insertCell(-1));
            cell.html(orderList[key].UrunKategorisiId).hide();

            cell = $(row.insertCell(-1));
            cell.html(orderList[key].UrunAdi);

            cell = $(row.insertCell(-1));
            cell.html(orderList[key].KategoriAdi);

            cell = $(row.insertCell(-1));
            cell.html(orderList[key].Fiyat).hide();

            cell = $(row.insertCell(-1));
            cell.html(orderList[key].StokMiktari).hide();

            cell = $(row.insertCell(-1));
            cell.html(orderList[key].Adet);

            cell = $(row.insertCell(-1));
            cell.html(orderList[key].ToplamFiyat);

            cell = $(row.insertCell(-1));
            cell.html(Tarihi);

            totalPrice++;
        }

        $('#toplamFiyat').show();
        $('#toplam_Fiyat').text('Toplam Fiyat: ' + totalPrice);
    }

    else {
        $('#orderListTable').hide();
        $('#order-info').show();
        $('#siperisOnaylaButton').hide();
        $('#toplamFiyat').hide()
        $("#item_MasaId").hide();

    }
});