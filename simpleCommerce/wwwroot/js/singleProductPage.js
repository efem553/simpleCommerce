function addItemtoCart(productId,count) {
    if (productId != null && count>0) {
        $.ajax({
            type: "GET",
            url: '/Cart/Additem?ProductId=' + productId + '&Count='+count,
            success: function (response) {
                getCartItemCount();
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Item added to card successfully',
                    showConfirmButton: false,
                    timer: 1500
                });
            },
            error: function () {
                console.log("Error while adding item to your cart");
            }
        });
    }
}