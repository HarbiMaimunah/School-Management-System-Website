$(document).ready(function () {
    $('#StudentsTable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Students/GetStudents",
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
            { "data": "enrollmentDate", "name": "Enrollment Date", "autoWidth": true, "orderable": false },
            { "data": "major", "name": "Major", "autoWidth": true, "orderable": false },
            { "data": "status", "name": "Status", "autoWidth": true, "orderable": false },
            { "data": "gpa", "name": "GPA", "autoWidth": true, "orderable": false },
            {
                "render": function (data, type, row) {
                    return "<button onclick=\"location.href = '/Students/Edit/" + row.id + "'\" class='btn btn-outline-primary'>Edit</button>"
                        + " <button onclick=\"location.href = '/Students/Delete/" + row.id + "'\" class='btn btn-outline-danger'>Delete</button> ";
                }
                , "searchable": false, "orderable": false
            }
        ]
    });
});

