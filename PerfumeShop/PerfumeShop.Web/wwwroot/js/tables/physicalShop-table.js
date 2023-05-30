var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#physicalShopData').DataTable({
        "ajax": {
            "url": "/Admin/ManagePhysicalShop/GetAll"
        },
        "columns": [
            { data: "address.state", "width": "20%" },
            { data: "address.city", "width": "20%" },
            { data: "address.streetAddress", "width": "20%" },
            { data: "address.postalCode", "width": "5%" },      
            { data: "openTime", "width": "5%" },
            { data: "closeTime", "width": "5%" },
            { data: "weekendsStr", "width": "20%" },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="btn-group" role="group" aria-label="Basic mixed styles example">
                    <a href="/Admin/ManagePhysicalShop/Edit?id=${data}" class="btn btn-outline-primary btn-sm shadow-none">
                    <i class="bi bi-wrench"></i> </a>
                    <a onClick=Delete('/Admin/ManagePhysicalShop/Delete/${data}') class="btn btn-outline-danger btn-sm shadow-none"> 
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