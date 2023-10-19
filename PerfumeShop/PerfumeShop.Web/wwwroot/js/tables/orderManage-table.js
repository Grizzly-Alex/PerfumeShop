var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#orderManageData').DataTable({
        "ajax": {
            "url": "/Admin/ManageOrder/GetAll"
        },
        "columns": [
            { data: "trackingId", "width": "9%" },
            { data: "dateFormat", "width": "8%" },
            { data: "orderStatus", "width": "5%" },
            { data: "paymentStatus", "width": "5%" },
            { data: "paymentMethod", "width": "10%" },
            { data: "customerName", "width": "10%" },
            { data: "customerPhone", "width": "5%" },
            { data: "customerEmail", "width": "10%" },
            { data: "address", "width": "25%" },
            { data: "itemsCostFormat", "width": "5%" },
            { data: "totalPriceFormat", "width": "5%" },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="btn-group" role="group" aria-label="Basic mixed styles example">
                    <a href="/Admin/ManageOrder/Details?id=${data}" class="btn btn-outline-success btn-sm shadow-none">
                    <i class="bi bi-info-lg"></i> </a>
					</div>`
                },
                "width": "3%"
            }

        ]
    });
}
