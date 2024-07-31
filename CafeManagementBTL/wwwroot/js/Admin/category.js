
$(document).ready(function () {
    loadData();
});

function loadData() {
    $('#tblCategory').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/admin/category/GetAllCategory',
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            {
                data: null,
                render: function (data, type, row) {
                    return `
                        <a href="/admin/category/edit/${data.id}" class="btn btn-sm btn-primary">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="/admin/category/delete/${data.id}" class="btn btn-sm btn-primary">
                            <i class="bi bi-trash"></i></span>Delete
                        </a>
                    `;

                },
                //`<a href="/admin/category/edit/${data.id}"><i class="bi bi-pencil-square"></i></a>`
                orderable: false, // This column should not be orderable
            },
        ],

    });
}

