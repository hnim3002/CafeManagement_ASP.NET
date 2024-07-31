$(document).ready(function () {
    loadData();
});

function loadData() {
    $('#tblCustomer').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/admin/customer/getallcustomer',
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'phoneNumber' },
            { data: 'email' },
            {
                data: null,
                render: function (data, type, row) {

                    return '<a href=' + '" ' + '/admin/customer/editcustomer/' + row.id + '"' + '><i class="bi bi-pencil-square"></i></a>';
                },
                orderable: false, // This column should not be orderable
            },
        ],

    });
}

