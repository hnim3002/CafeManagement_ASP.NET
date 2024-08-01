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
            url: '/manager/user/getalluser',
        },
        columns: [
            { data: 'id' },
            { data: 'fullName' },
            { data: 'phoneNumber' },
            { data: 'role' },
            { data: 'address' },
            { data: 'dateOfBirth' },
            { data: 'cafe.name' },
           
        ],
    });
    $('#roleFilter').on('change', function () {
        var selectedRole = $(this).val();
        table.column(3).search(selectedRole).draw(); 
    });

}


