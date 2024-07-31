
$(document).ready(function () {
    loadData();
});

function loadData() {
    $('#tblCategory').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/staff/category/GetAllCategory',
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            //{
            //    data: null,
            //    render: function (data, type, row) {
            //        return 0;

            //    },
            //    //`<a href="/admin/category/edit/${data.id}"><i class="bi bi-pencil-square"></i></a>`
            //    orderable: false, // This column should not be orderable
            //},
        ],

    });
}

