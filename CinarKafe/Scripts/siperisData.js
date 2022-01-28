$(document).ready(function () {

    var sepeteEkleButton = $(".sepeteEkleButton");
    var increaseOrderButton = $(".increaseOrderButton");
    var decreaseOrderButton = $(".decreaseOrderButton");

    var userid = $("#CurrentUserId").attr('value');

    var ordercountkey = userid + "-OrderCount";

    var ordercount = localStorage.getItem(ordercountkey);

    if (ordercount !== null) {
        $(".siperisCount").text(ordercount);
    }

    increaseOrderButton.click(function () {
        var id = $(this).attr("id").split('-')[1];

        var productItem = $("#product-" + id);
        var maxStokValue = productItem.find("input#stokAmount").attr('value');

        // increase count and update 
        $("#Adet-" + id).val(function (i, oldVal) {
            if (parseInt(oldVal, 10) < parseInt(maxStokValue,10)) {
                return parseInt(oldVal, 10) + 1;
            }
        });
    });


    decreaseOrderButton.click(function () {
        var id = $(this).attr("id").split('-')[1];

        // increase count and update 
        $("#Adet-" + id).val(function (i, oldVal) {
            if (parseInt(oldVal, 10) > 1) {
                return parseInt(oldVal, 10) - 1;
            }
        });
    });

    var siperisObjectList = [];

    sepeteEkleButton.click(function () {
        var id = $(this).attr("id");

        var productItem = $("#product-" + id);

        // get product details 

        // - Urun Id
        var UrunId = id;

        // - Urun adi  
        var UrunAdi = productItem.find('h5#UrunAdi').text();

        // - Fiyat
        var Fiyat = parseInt(productItem.find('h5#Fiyat').text(), 10);

        // - Stok miktari 
        var StokMiktari = productItem.find('input#stokAmount').attr('value');

        // - Urun Kategorisi Adi 
        var UrunKategoriAdi = productItem.find('input#UrunKategoriAdi').attr('value');

        // - Urun kategori Id 
        var UrunKategorisiId = productItem.find('input#UrunKategoriId').attr('value');

        // - Adet 
        var Adet = parseInt(productItem.find('input#Adet-' + id).val(), 10);

        // - Toplam fiyat 
        var ToplamFiyat = Fiyat * Adet;

        // - Get user id 
        var UserId = productItem.find("input#UserId").attr('value');

        siperisObjectList.unshift(
            {
                "UrunId": UrunId,
                "UrunKategorisiId": UrunKategorisiId,
                "UrunAdi": UrunAdi,
                "KategoriAdi": UrunKategoriAdi,
                "Fiyat": Fiyat,
                "StokMiktari": StokMiktari,
                "Adet": Adet,
                "ToplamFiyat": ToplamFiyat
            }
        );

        var orderKey = UserId + "-Orders";

        localStorage.setItem(orderKey, JSON.stringify(siperisObjectList));
        //localStorage.clear();
        // Update nav count 
        var siperisCount = $(".siperisCount");
        var orderCount = parseInt(siperisCount.text(), 10) + Adet;

        siperisCount.text(orderCount);

        // store count to local storage (In case of page reloads) - use sessions

        var orderCountKey = UserId + "-OrderCount";

        localStorage.setItem(orderCountKey, `${orderCount}`);
    });
   
});
