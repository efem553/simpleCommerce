$(document).ready(function () {
    loadDataTable("GetCategoryList");
});

function loadDataTable(url) {
    $("#categoryTable").DataTable({
        "ajax": {
            "url": "/Category/" + url
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "filterName", "width": "40%" },
            {
                "data": "categoryId",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="~/Category/Edit/${data}" class="addButton text-white" style="cursor:poiner">
                            <i class="fas fa-edit"></i>
                        </a>
                    </div>
                    `
                },
                "width": "20%"
            }
        ]
    });
    $.ajax({
        method: 'GET',
        headers: { 'CategoryId': '70e9dfda-4982-4b88-96f9-d7d284a10cb4' },
        url: '/home/sampleaction'
    });
}
