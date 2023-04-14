var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#userData').DataTable({
        "ajax": {
            "url": "/Admin/ManageUser/GetAll"
        },
        "columns": [
            { data: "role", "width": "15%" },
            { data: "userName", "width": "10%" },
            { data: "email", "width": "10%" },
            { data: "firstName", "width": "10%" },
            { data: "lastName", "width": "10%" },
            { data: "streetAddress", "width": "10%" },
            { data: "city", "width": "10%" },
            { data: "state", "width": "10%" },
            { data: "phoneNumber", "width": "10%" },
            { data: "postalCode", "width": "10%" },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="btn-group" role="group" aria-label="Basic mixed styles example">
                    <a href="/Admin/ManageUser/edit?id=${data}" class="btn btn-outline-primary btn-sm shadow-none">
                    <i class="bi bi-wrench"></i> </a>
                    <a onClick=Delete('/Admin/ManageUser/Delete/${data}') class="btn btn-outline-danger btn-sm shadow-none"> 
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