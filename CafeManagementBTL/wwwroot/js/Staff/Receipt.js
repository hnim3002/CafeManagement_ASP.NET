$(document).ready(function () {
    loadData();

    $('#option').on('keyup', function () {
        $('#tblUser').DataTable().draw();
    });

});

function loadData() {

    var table = $('#tblReceipt').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/staff/Receipt/GetAllReceipts',
        },
        columns: [
            { data: 'id' },
            { data: 'date' },
            { data: 'employeeId' },
            { data: 'cafe.name' },
            { data: 'total' },
            { data: 'discount' },
            { data: 'tax' },
            { data: 'finalTotal' },
            {
                data: null,
                render: function (data, type, row) {

                    return `
                        <div class="d-flex">
                            <a href="/staff/Receipt/Detail/${row.id}" class="btn btn-primary">
                                Detail
                            </a>
                            <button class="btn btn-danger btn-delete" data-id="${row.id}">
                                <i class="bi bi-x-lg"></i>
                            </button>
                        </div>
                            ` ;
                },
                orderable: false, // This column should not be orderable
            },
        ],
    });
    $('#roleFilter').on('change', function () {
        var selectedRole = $(this).val();
        table.column(3).search(selectedRole).draw();
    });

    $('#tblReceipt tbody').on('click', '.btn-delete', function () {
        var userId = $(this).data('id');
        console.log(userId)
        if (confirm("Bạn có chắc chắn muốn xóa sản phẩm này không?")) {
            $.ajax({
                url: `/staff/receipt/delete/${userId}`,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        var row = $(`#tblReceipt tbody .btn-delete[data-id="${userId}"]`).closest('tr');
                        table.row(row).remove().draw();
                    } else {
                        console.error('Error:', data.message);
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        }
    });
}


