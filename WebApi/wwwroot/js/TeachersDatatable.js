$(document).ready(function () {
    $('#TeachersTable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Teachers/GetTeachers",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "ID", "autoWidth": true },
            { "data": "firstName", "name": "First Name", "autoWidth": true },
            { "data": "lastName", "name": "Last Name", "autoWidth": true, "orderable": false },
            { "data": "birthDate", "name": "Birth Date", "autoWidth": true, "orderable": false },
            { "data": "gender", "name": "Gender", "autoWidth": true, "orderable": false },
            { "data": "department", "name": "Department", "autoWidth": true, "orderable": false },
            { "data": "salary", "name": "Salary", "autoWidth": true, "orderable": false },
            {
                "render": function (data, type, row) {
                    return "<button onclick=\"location.href = '/Teachers/Edit/" + row.id + "'\" class='btn btn-outline-primary'>Edit</button>"
                        + " <button onclick=\"location.href = '/Teachers/Delete/" + row.id + "'\" class='btn btn-outline-danger'> Delete</button> ";
                }
                , "searchable": false, "orderable": false
            }
        ]
    });
});
