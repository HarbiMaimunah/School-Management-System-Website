$(document).ready(function () {
    $('#CoursesTable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Courses/GetCourses",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": target,
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "ID", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "creditHours", "name": "Credit Hours", "autoWidth": true, "orderable": false },
            {
                "render": function (data, type, row) {
                    
                        return "<button onclick=\"location.href = '/Courses/Edit/" + row.id + "'\" class='btn btn-outline-primary'>Edit</button>"
                            + " <button onclick=\"location.href = '/Courses/Delete/" + row.id + "'\" class='btn btn-outline-danger'> Delete</button> "; 
                    
                }
                    , "searchable": false, "orderable": false

            }
    
        ]
    });
});




