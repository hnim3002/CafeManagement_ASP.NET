$(document).ready(function () {
    loadData();
});

function loadData() {
    $('#tblCafe').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/staff/cafe/getallcafe',
        },
        columns: [
            { data: 'id'},
            { data: 'name'},
            { data: 'address' },  
        ],

    });
}


