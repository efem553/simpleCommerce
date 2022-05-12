var datatable;
var propertyTable;
var tagTable;
function onPageLoad () {
    loadDataTable("GetProductList");
    //Property Table Init.
    loadPropertyTable();
    //Tag Table Init.
    loadTagTable();
}

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
        "ajax": {
            "url": "/Product/GetProductProperty?id="+$('#Product_Id').val()
        },
        "columns": [
            { "data": "propertyId", "visible":false},
            { "data": "name", "width": "40%"},
            { "data": "value", "width": "40%" },
            {
                "data": "button",
                "render": function (data) {
                    return `
                    <div class="row">
      
                        <a  class="badge-trashed remove" style="cursor:pointer" onclick="DeleteProductProperty('`+data+`');">
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

function addRowToPropertyTable(productId, propertyId, value) {
    //console.log('/Product/AddProductProperty?productId=' + productId + '&propertyId=' + propertyId + '&value=' + value);
    var isAdded;
    for (i = 0; i < $('#propertyTable').DataTable().rows().data().count(); i++)
    {
        if ($('#propertyTable').DataTable().rows().data()[i]['propertyId'] === propertyId) {
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
        $.ajax({
            type: 'GET',
            url: '/Product/AddProductProperty?productId=' + productId + '&propertyId='+ propertyId+'&value='+value,
            complete: function (xhr) {
                if (xhr.status === 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Record added successfully!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    propertyTable.ajax.reload();
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
    
}

function DeleteProductProperty(guid) {
    Swal.fire({
        title: 'This record gonna be deleted. Are u sure?',
        showCancelButton: true,
        confirmButtonText: 'Yes',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                type: 'GET',
                url: '/Product/DeleteProductProperty?id=' + guid,
                complete: function (xhr) {
                    if (xhr.status === 200) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Record deleted successfully!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        propertyTable.ajax.reload();
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

function loadTagTable() {
    tagTable = $("#tagTable").DataTable({
        "ajax": {
            "url": "/Product/GetProductTag?id=" + $('#Product_Id').val()
        },
        "columns": [
            { "data": "tagId", "visible": false },
            { "data": "name", "width": "40%" },

            {
                "data": "button",
                "render": function (data) {
                    return `
                    <div class="row">
    
                        <a  class="badge-trashed remove" style="cursor:pointer" onclick="DeletePropertyTag('`+data+`')">
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

function addRowToTagTable(productId, tagId) {
    //console.log(prodoductId, tagId);
    var isAdded;
    for (i = 0; i < $('#tagTable').DataTable().rows().data().count(); i++) {
        if ($('#tagTable').DataTable().rows().data()[i]['tagId'] === tagId) {
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
        $.ajax({
            type: 'GET',
            url: '/Product/AddProductTag?productId=' + productId + '&tagId=' + tagId,
            complete: function (xhr) {
                if (xhr.status === 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Record added successfully!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    tagTable.ajax.reload();
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
}

function DeletePropertyTag(guid) {
    Swal.fire({
        title: 'This record gonna be deleted. Are u sure?',
        showCancelButton: true,
        confirmButtonText: 'Yes',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                type: 'GET',
                url: '/Product/DeleteProductTag?id=' + guid,
                complete: function (xhr) {
                    if (xhr.status === 200) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Record deleted successfully!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        tagTable.ajax.reload();
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