var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#userOrderData').DataTable({
        "ajax": {
            "url": "/UserOrderManage/GetAll"
        },
        "columns": [
            { data: "trackingId", "width": "5%" },
            { data: "orderDate", "width": "12%" },
            { data: "orderStatus", "width": "5%" },
            { data: "paymentStatus", "width": "5%" },
            { data: "paymentMethod", "width": "5%" },
            { data: "customerName", "width": "5%" },
            { data: "customerPhone", "width": "5%" },
            { data: "customerEmail", "width": "8%" },
            { data: "address", "width": "10%" },
            { data: "itemsCost", "width": "5%" },
            { data: "totalPrice", "width": "5%" },

        ]
    });
}
