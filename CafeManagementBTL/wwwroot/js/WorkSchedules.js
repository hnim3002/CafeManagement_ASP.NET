$(document).ready(function () {
    loadData();
});

function loadData() {
    $('#tblWorkSchedules').DataTable({
        destroy: true,  // Ensure any existing table is destroyed before reinitializing
        ajax: {
            url: '/admin/workschedules/getallworkschedules',

        },
        columns: [
            { data: 'id' },
            { data: 'date' },
            { data: 'startTime' },
            { data: 'endTime' },
            { data: 'type' },
            { data: 'note' },
            { data: 'employeeId' },
            { data: 'cafeId' },
            {
                data: null,
                render: function (data, type, row) {

                    return '<a href=' + '" ' + '/admin/workschedules/editworkschedules/' + row.id + '"' + '><i class="bi bi-pencil-square"></i></a>';
                },
                orderable: false, // This column should not be orderable
            },
        ],
    });
}
