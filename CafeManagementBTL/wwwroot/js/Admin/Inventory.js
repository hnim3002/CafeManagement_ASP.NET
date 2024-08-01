$(document).ready(function () {
    loadData();
});

function loadData() {
    $('#tblInventory').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/admin/inventory/GetAllInventory',
        },
        columns: [
            { data: 'product.name' },
            { data: 'quantity' },
            { data: 'cafe.name' },
            {data: 'price'},
            {
                data: null,
                render: function (data, type, row) {
                    var html = `
                        <form action="/admin/inventory/delete" class="delete-form" method="post" style="display:inline;">
                        <input type="hidden" name="productId" value="${data.product.id}" />
                        <input type="hidden" name="cafeId" value="${data.cafe.id}" />
                            <button type="submit" class="btn btn-sm btn-dark">
                                <i class="bi bi-trash"></i> Delete
                            </button>
                    </form>
                        `;
                    
                    return html;
                    //<form action="/admin/inventory/edit/${data.product.id}-${data.cafe.id}" method="get" class="edit-form" style="display:inline;">
                    //    <button type="submit" class="btn btn-sm btn-primary">
                    //        <i class="bi bi-pencil-square"></i> Edit
                    //    </button>
                    //</form>
                },
                //`<a href="/admin/inventory/edit/${data.product.id}-${data.cafe.id}" class="btn btn-sm btn-primary"><i class="bi bi-pencil-square"></i> Edit</a><button class="btn btn-sm btn-danger btn-delete" data-product-id="${data.product.id}" data-cafe-id="${data.cafe.id}"><i class="bi bi-trash"></i> Delete</button>`;
                orderable: false, // This column should not be orderable
            },
        ],

    });
}

$(document).ready(function () {
    loadData();

    // Bắt sự kiện submit của form xóa
    $(document).on('submit', '.delete-form', function (e) {
        e.preventDefault(); // Ngăn chặn hành động mặc định của form

        var form = this;

        // Hiển thị hộp thoại xác nhận
        if (confirm('Are you sure you want to delete this item?')) {
            form.submit(); // Nếu người dùng đồng ý, gửi form
        }
    });
});


