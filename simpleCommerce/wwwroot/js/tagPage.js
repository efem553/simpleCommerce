var datatable;
$(document).ready(function () {
    loadDataTable("GetTagList");
});

function loadDataTable(url) {
    datatable = $("#tagTable").DataTable({
        "ajax": {
            "url": "/Tag/" + url
        },
        "columns": [
            { "data": "tagId", "width": "0%", "visible": false },
            { "data": "name", "width": "40%" },
            { "data": "filterName", "width": "40%" },
            {
                "data": "tagId",
                "render": function (data) {
                    return `
                    <div class="row">
                        <a href="/Tag/Edit?id=${data}"  class="badge-pending" style="cursor:pointer; margin-right:2%;">
                            Edit
                        </a>
            
                        <a onclick="DeleteTag('${data}');" class="badge-trashed" style="cursor:pointer">
                            Delete
                        </a>
                    </div>
                    `
                },
                "width": "20%"
            }
        ]
    });
}



function DeleteTag(guid) {
    Swal.fire({
        title: 'This record gonna be deleted. Are u sure?',
        showCancelButton: true,
        confirmButtonText: 'Yes',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                type: 'GET',
                url: '/Tag/DeleteTag?id=' + guid,
                complete: function (xhr) {
                    if (xhr.status === 200) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Record deleted successfully!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        datatable.ajax.reload();
                    }
                    else {
                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: 'Something went wrong!',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                }
            });

        }
    })

}


