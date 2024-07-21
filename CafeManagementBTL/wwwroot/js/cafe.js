$(document).ready(function () {
    loadData();
});

function loadData() {
    $('#tblCafe').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/admin/cafe/getallcafe',
        },
        columns: [
            { data: 'id'},
            { data: 'name'},
            { data: 'address' },
            {
                data: null, 
                render: function (data, type, row) {
                 
                    return '<i class="bi bi-pencil-square"></i>';
                },
                orderable: false, // This column should not be orderable
            },
        ],

    });
}
