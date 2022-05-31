var priceRangeSlider;
var sortingSelect
$(document).ready(function () {
    setFilterValues();
});

$('#applyFilter').on('click', function () {
    applyFilters();
});

$('#searchButton').on('click', function () {
    applyFilters();
});

function applyFilters() {
    var categoryId = $('input[name="category"]:checked').attr("id");
    var tagId = $('input[name="tag"]:checked').attr("id");
    var lowestprice = $("#lower-value")[0].innerText;
    var highestPrice = $("#upper-value")[0].innerText;
    var sortingFilter = $("#sortingSelect").val();
    var searchFilter = $("#productSearch").val();
    var urlParams = "";
    if (categoryId != null) {
        urlParams += "category=" + categoryId + "&";
    }
    if (tagId != null) {
        urlParams += "tag=" + tagId + "&";
    }
    if (lowestprice != null || highestPrice != null) {
        urlParams += "price=" + lowestprice + "," + highestPrice + "&";
    }
    if (sortingFilter != null) {
        urlParams += "order=" + sortingFilter + "&";
    }
    if (searchFilter != null) {
        urlParams += "searchFilter=" + searchFilter + "&";
    }
    window.location.href = "?"+urlParams;
}


function setFilterValues() {
    var slider = $('#price-range');
    var categoryId;
    var tagId;
    var lowestprice;
    var highestPrice;
    var sortingFilter;
    var searchFilter;
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    if (urlParams.get('category') != null) {
        categoryId = urlParams.get('category');
        $('input[name="category"][id="' + categoryId + '"]').attr("checked", "checked");
    };
    if (urlParams.get('tag') != null) {
        tagId = urlParams.get('tag');
        $('input[name="tag"][id="' + tagId + '"]').attr("checked", "checked");
    };
    if (urlParams.get('price') != null) {
        lowestPrice = urlParams.get('price').split(",")[0];
        highestPrice = urlParams.get('price').split(",")[1];
        if (lowestPrice != null && highestPrice != null) {
            priceRangeSlider.set([lowestPrice, highestPrice]);
        }
    }

    if (urlParams.get('order') != null) {
        sortingFilter = parseInt(urlParams.get('order'));
        $('#sortingSelect').niceSelect('update')[0].options.selectedIndex = sortingFilter;
 
    }
    if (urlParams.get('searchFilter') != null) {
        searchFilter = urlParams.get('searchFilter');
        $("#searchFilter").val(searchFilter);
    }

    //------- Active Nice Select --------//
    $('select').niceSelect();
}
function addItemtoCart(productId) {
    if (productId != null) {
        $.ajax({
            type: "GET",
            url: '/Cart/Additem?ProductId=' + productId + '&Count=1',
            success: function (response) {
                getCartItemCount();
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Product added to cart successfully',
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
