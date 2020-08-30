var table = null
var selectList = null

function callTable() {
    table = $('#myTable').DataTable({
        "ajax": {
            'url': "/Divisions/Load",
            'type': "GET",
            'dataType': "json",
            'dataSrc': ""
        },
        "columns": [
            {
                "data": "id", defaultContent: ''
            },
            { "data": "Name" },
            { "data": "Department_Name"},
            {
                "sortable": false,
                "render": function myFunction(data, type, row) {
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button id="update" class="btn btn-warning" data-toggle="tooltip" data-placement="top" title="Update" onClick="Update(' + row.Id + ')"><i class="fa fa-pen"></i></button>' +
                        '&nbsp;' +
                        '<button id="delete" class="btn btn-danger" data-toggle="tooltip" data-placement="top" title="Delete" onClick="Delete(' + row.Id + ')"><i class="fa fa-trash"></i></button>'
                }
            }],
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']]
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

$('#insertButton').click(function () {
    $('.clearFields').val('');
    $.getJSON('/Departments/Load', { Id: $(this).val() }, function (data) {
        var options = '<option value="0" selected disabled>Select Department</option>';
        for (var x = 0; x < data.length; x++) {
            options += '<option value="' + data[x]['Id'] + '">' + data[x]['Name'] + '</option>';
        }
        $('#departmentId').html(options);
    });
});

$(document).ready(function () {
    debugger;
    callTable();
    $.getJSON('/Departments/Load', { Id: $(this).val() }, function (data) {
        var options = '<option value="0" selected disabled>Select Department</option>';
        for (var x = 0; x < data.length; x++) {
            options += '<option value="' + data[x]['Id'] + '">' + data[x]['Name'] + '</option>';
        }
        $('#departmentId').html(options);
    });
});

//$('#departmentId').on("click change", function () {
//    $.getJSON('/Departments/Load', { Id: $(this).val() }, function (data) {
//        var options = '';
//        for (var x = 0; x < data.length; x++) {
//            options += '<option value="' + data[x]['Id'] + '">' + data[x]['Name'] + '</option>';
//        }
//        $('#departmentId').html(options);
//    });
//})

$('#insert').click(function () {
    debugger;
    $.ajax({
        url: "/Divisions/Insert/" + $('#divisionId').val(),
        type: "post",
        data: {
            'name': $('#divisionName').val(),
            'department': {
                'Id': $('#departmentId').children("option:selected").val()
            }  
        },
        dataType: "json",
        success: function (response) {
            if (response.success === true) {
                swal("Success!", "Data is inserted", "success");
                table.ajax.reload();
                $('.clearFields').val('');
            }
            else {
                swal("Error!", "Data is not inserted", "error");
            }
        },
        error: function () {
            swal("Error!", "Data is not inserted", "error");
        }
    });

});

function Update(id) {
    debugger;
    $('#insertModal').modal('show');
    $.ajax({
        url: "/Divisions/LoadId/" + id,
        type: "get",
        success: function (response) {
            $('#divisionId').val(response.Id);
            $('#divisionName').val(response.Name);
            $('#departmentId').val(response.Department_Id);
        }
    });
};

function Delete(id) {
    debugger;
    swal({
        title: "Are you sure?",
        text: "This cannot be undone!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: "/Divisions/Delete/" + id,
                type: "post",
                success: function (result) {

                }
            });
            swal("Your file has been deleted!", {
                icon: "success",
            });
            table.ajax.reload();
        } else {

        }
    });
};



