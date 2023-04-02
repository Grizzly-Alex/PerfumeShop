var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productData').DataTable({
        "ajax": {
            "url": "/admin/manageproduct/getall"
        },
        "columns": [
            { data: "name", "width": "15%" },
            { data: "brand", "width": "10%" },
            { data: "gender", "width": "10%" },
            { data: "aroma type", "width": "10%" },
            { data: "release form", "width": "10%" },
            { data: "price", "width": "10%" },
            { data: "volume", "width": "10%" },
            { data: "stock", "width": "10%" },
            { data: "date delivery", "width": "10%" },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="btn-toolbar justify-content-between" role="toolbar">
                    <a href="/admin/managebrand/edit?id=${data}" class="btn btn-outline-primary btn-sm shadow-none">
                    <i class="bi bi-wrench"></i> </a>
                    <a onClick=Delete('/admin/managebrand/delete/${data}') class="btn btn-outline-danger btn-sm shadow-none"> 
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
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })

        }
    })
}