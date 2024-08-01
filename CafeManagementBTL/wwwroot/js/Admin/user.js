$(document).ready(function () {
    loadData();

    $('#option').on('keyup', function () {
        $('#tblUser').DataTable().draw();
    });

});

function loadData() {

    var table = $('#tblUser').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/admin/user/getalluser',
        },
        columns: [
            { data: 'id' },
            { data: 'fullName' },
            { data: 'phoneNumber' },
            { data: 'role' },
            { data: 'address' },
            { data: 'dateOfBirth' },
            { data: 'cafe.name' },
            {
                data: null,
                render: function (data, type, row) {

                    return `
                        <div class="d-flex">
                            <a href="/admin/user/edit/${row.id}" class="btn btn-primary">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a href="/admin/user/passchange" class="btn btn-secondary">
                                Change Password
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

    $('#tblUser tbody').on('click', '.btn-delete', function () {
        var userId = $(this).data('id');
        console.log(userId)
        if (confirm("Bạn có chắc chắn muốn xóa sản phẩm này không?")) {
            $.ajax({
                url: `/admin/user/delete/${userId}`,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        var row = $(`#tblUser tbody .btn-delete[data-id="${userId}"]`).closest('tr');
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


