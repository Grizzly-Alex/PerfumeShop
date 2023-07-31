var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#orderHistoryData').DataTable({
        "ajax": {
            "url": "/OrderHistory/GetAll"
        },
        "columns": [
            { data: "trackingId", "width": "5%" },
            { data: "orderDate", "width": "12%" },
            { data: "orderStatus", "width": "5%" },
            { data: "paymentStatus", "width": "5%" },
            { data: "paymentMethod", "width": "10%" },
            { data: "customerName", "width": "10%" },
            { data: "customerPhone", "width": "5%" },
            { data: "customerEmail", "width": "10%" },
            { data: "address", "width": "25%" },
            { data: "itemsCost", "width": "5%" },
            { data: "totalPrice", "width": "5%" },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="btn-group" role="group" aria-label="Basic mixed styles example">
                    <a href="/OrderHistory/Details?id=${data}" class="btn btn-outline-success btn-sm shadow-none">
                    <i class="bi bi-info-lg"></i> </a>
					</div>`
                },
                "width": "3%"
            }

        ]
    });
}
