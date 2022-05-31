function sendJsonCartItems() {
    jsonObj = [];
    $('input[name=qty]').each(function() {
        var id = $(this).attr('id');
        var quantity = parseInt($(this).val());
        item = {};
        item['cartItemId'] = id;
        item['quantity'] = quantity;

        jsonObj.push(item);

    });
    //console.log(jsonObj);
    $.ajax({
        type: "GET",
        url: '/Cart/Update?cartItemsJSON='+JSON.stringify(jsonObj),
        success: function (response) {
            window.location.href = "/Cart";
        },
        error: function () {
            window.location.href = "/Cart";
            console.error("Something went wrong while updating cart");
        }
    });
}
