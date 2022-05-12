var datatable;
var propertyTable;
var tagTable;
$(document).ready(function () {
    loadDataTable("GetProductList");
    //Property Table Init.
    loadPropertyTable();
    $('#propertyTable').on('click', '.remove', function () {
        var table = $('#propertyTable').DataTable();
        var row = $(this).parents('tr');

        if ($(row).hasClass('child')) {
            table.row($(row).prev('tr')).remove().draw();
        }
        else {
            table
                .row($(this).parents('tr'))
                .remove()
                .draw();
        }

    });
    //Tag Table Init.
    loadTagTable();
    $('#tagTable').on('click', '.remove', function () {
        var table = $('#tagTable').DataTable();
        var row = $(this).parents('tr');

        if ($(row).hasClass('child')) {
            table.row($(row).prev('tr')).remove().draw();
        }
        else {
            table
                .row($(this).parents('tr'))
                .remove()
                .draw();
        }

    });
});

function loadDataTable(url) {
    datatable = $("#productTable").DataTable({
        "ajax": {
            "url": "/Product/" + url
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "isInStock", "width": "15%" },
            { "data": "stockCount", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="row">
                        <a href="/Product/Edit?id=${data}"  class="badge-pending" style="cursor:pointer; margin-right:2%;">
                            Edit
                        </a>
            
                        <a onclick="DeleteProduct('${data}');" class="badge-trashed" style="cursor:pointer">
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

function DeleteProduct(guid) {
    Swal.fire({
        title: 'This record gonna be deleted. Are u sure?',
        showCancelButton: true,
        confirmButtonText: 'Yes',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                type: 'GET',
                url: '/Product/DeleteProduct?id=' + guid,
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

function loadPropertyTable() {
    propertyTable = $("#propertyTable").DataTable({
        "columns": [
            { "data": "propertyId", "visible":false},
            { "data": "name", "width": "40%"},
            { "data": "value", "width": "40%" },
            {
                "data": "button",
                "render": function (data) {
                    return `
                    <div class="row">
      
                        <a  class="badge-trashed remove" style="cursor:pointer">
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

function addRowToPropertyTable(id, name, value) {
    var isAdded;
    for (i = 0; i < $('#propertyTable').DataTable().rows().data().count(); i++)
    {
        if ($('#propertyTable').DataTable().rows().data()[i]['propertyId'] === id) {
            isAdded = true;
            break;
        }
    }

    if (isAdded) {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'This property already added!',
            showConfirmButton: false,
            timer: 1500
        });
    }
    else {
        propertyTable.row.add({
            "propertyId": id,
            "name": name,
            "value": value,
            "button": null
        }).draw();
    }
    
}

function loadTagTable() {
    tagTable = $("#tagTable").DataTable({
        "columns": [
            { "data": "tagId", "visible": false },
            { "data": "name", "width": "40%" },

            {
                "data": "button",
                "render": function (data) {
                    return `
                    <div class="row">
    
                        <a  class="badge-trashed remove" style="cursor:pointer">
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

function addRowToTagTable(id, name) {
    var isAdded;
    for (i = 0; i < $('#tagTable').DataTable().rows().data().count(); i++) {
        if ($('#tagTable').DataTable().rows().data()[i]['tagId'] === id) {
            isAdded = true;
            break;
        }
    }

    if (isAdded) {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'This property already added!',
            showConfirmButton: false,
            timer: 1500
        });
    }
    else {
        tagTable.row.add({
            "tagId": id,
            "name": name,
            "button": null
        }).draw();
    }
}

function showPreview(event) {
    if (event.target.files.length > 0) {
        for (i = 0; i < event.target.files.length; i++)
        {
            var src = URL.createObjectURL(event.target.files[i]);
            var block = document.getElementById("preview");
            
            var preview = document.createElement('img');
            preview.src = src;
            preview.className = "previewImage";
            preview.style.display = "block";
            block.appendChild(preview);
        }
    }
}

function exportAsJsonString() {
    var objectArray=[];
    for (i = 0; i < $('#propertyTable').DataTable().rows().data().count(); i++) {
        objectArray.push($('#propertyTable').DataTable().rows().data()[i]);
        
        
    }
    //console.log(objectArray);
    $('#PropertyJSON').val(JSON.stringify(objectArray));
    objectArray = [];
    for (i = 0; i < $('#tagTable').DataTable().rows().data().count(); i++) {
        objectArray.push($('#tagTable').DataTable().rows().data()[i]);


    }
    //console.log(objectArray);
    $('#TagJSON').val(JSON.stringify(objectArray));

}

function onSave() {
    exportAsJsonString();
    $('#createProductForm').submit();
}

function openTab(evt, tab) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(tab).style.display = "block";
    evt.currentTarget.className += " active";
    $("#propertyTable").DataTable().columns.adjust().draw();
    evt.currentTarget.scrollIntoView({ behavior: 'smooth', block: 'center' });
}