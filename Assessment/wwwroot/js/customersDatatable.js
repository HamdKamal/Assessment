$(document).ready(function () {
    $('#example').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/customers",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "ID", "name": "ID", "autowidth": true },
            { "data": "Name", "name": "Name", "autowidth": true },
            { "data": "Department", "name": "Department", "autowidth": true },
            { "data": "Phone", "name": "Phone", "autowidth": true },
            { "data": "Email", "name": "Email", "autowidth": true },
            { "data": "Date_Created", "name": "Date_Created", "autowidth": true },
            { "data": "Created_By", "name": "Created_By", "autowidth": true },
            //{ "data": "Status", "name": "Status", "autowidth": true },
            {
                "render": function (data, type, row) { return '<span>' + row.dateOfBirth.split('T')[0] + '</span>' },
                "name": "Status"
            },
            {
                "render": function (data, type, row) { return '<a href="#" class="btn btn-danger" onclick=DeleteCustomer("' + row.ID + '"); > Delete </a>' },
                "orderable": false
            },
            
        ]
    });
});