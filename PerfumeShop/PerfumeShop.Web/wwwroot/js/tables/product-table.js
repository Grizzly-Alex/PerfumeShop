var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productData').DataTable({
        "ajax": {
            "url": "/Admin/ManageProduct/GetAll"
        },
        "columns": [
            { data: "name", "width": "15%" },
            { data: "price", "width": "10%" },
            { data: "volume", "width": "10%" },
            { data: "stock", "width": "10%" },      
            { data: "brand.name", "width": "10%" },
            { data: "gender.name", "width": "10%" },
            { data: "aromaType.name", "width": "10%" },
            { data: "releaseForm.name", "width": "10%" },
            { data: "dateDelivery", "width": "10%" },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="btn-group" role="group" aria-label="Basic mixed styles example">
                    <a href="/Admin/ManageProduct/Details?id=${data}" class="btn btn-outline-success btn-sm shadow-none">
                    <i class="bi bi-info-lg"></i> </a>
                    <a href="/Admin/ManageProduct/Edit?id=${data}" class="btn btn-outline-primary btn-sm shadow-none">
                    <i class="bi bi-wrench"></i> </a>
                    <a onClick=Delete('/Admin/ManageProduct/Delete/${data}') class="btn btn-outline-danger btn-sm shadow-none"> 
                    <i class="bi bi-trash3"></i> </a>
					</div>`
                },
                "width": "5%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })

        }
    })
}