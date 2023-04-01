var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#brandData').DataTable({
        "ajax": {
            "url": "/Admin/ManageBrand/GetAll"
        },
        "columns": [
            { "data": "name", "width": "95%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-toolbar justify-content-between" role="toolbar">
                        <a href="/Admin/ManageBrand/Edit?id=${data}"
                        class="btn btn-outline-primary btn-sm shadow-none"> <i class="bi bi-wrench"></i> </a>
                        <a onclick=Delete('/Admin/ManageBrand/Delete/${data}')
                        class="btn btn-outline-danger btn-sm shadow-none"> <i class="bi bi-trash3"></i> </a>
					    </div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}