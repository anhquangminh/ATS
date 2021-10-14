//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
    ClearHistory();
});

function ClearHistory() {
    var backlen = history.length;
    history.go(-backlen);
    window.location.href = loggedOutPageUrl
}

//Load Data function  
function loadData() {
    $.ajax({
        url: "/ATS/JsonWindowOS",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (result) {
            var html = '';
            var stt = 0;
            $.each(result, function (key, item) {
                stt = stt + 1;
                html += '<tr>';
                html += '<td style="text-align:center;" ><a href="#" onclick="return GetbyID(\'' + item.Package + '\')"><i class="fas fa-edit"></i></a></td>';
                html += '<td style="text-align:center;"> <a href="#" onclick="DeleteWindowOS(\'' + item.Package + '\')"><i class="far fa-trash-alt"></i></a> </td>';
                html += '<td>' + item.Owner + '</td>';
                html += '<td>' + item.Package + '</td>';
                html += '<td>' + item.WindowOS + '</td>';
                html += '<td>' + item.MSrelease + '</td>';
                html += '<td>' + item.ITrelease + '</td>';
                html += '<td>' + item.Input_date + '</td>';
                html += '</tr>';
            });
            $('#tbody').html(html);
            //$('#table_ga').DataTable({
            //    initComplete: function () {
            //        this.api().columns().every(function () {
            //            var column = this;
            //            var select = $('<select><option value=""></option></select>')
            //                .appendTo($(column.footer()).empty())
            //                .on('change', function () {
            //                    var val = $.fn.dataTable.util.escapeRegex(
            //                        $(this).val()
            //                    );

            //                    column
            //                        .search(val ? '^' + val + '$' : '', true, false)
            //                        .draw();
            //                });

            //            column.data().unique().sort().each(function (d, j) {
            //                select.append('<option value="' + d + '">' + d + '</option>')
            //            });
            //        });
            //    }
            //});

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    var table = $('#table_ga').DataTable({
        ajax: "data.json"
    });

    table.ajax.url('newData.json').load();
}

//Add Data Function   
function Add() {
    $('#myModalLabel').text('Add new infor owner');
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        emp: $('#emp').val(),
        name_vn: $('#name_vn').val(),
        building: $('#building').val(),
        team: $('#team').val(),
    };
    $.ajax({
        url: "/ATS/AddOwner",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            alert(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based   
function GetbyID(id) {
    $.ajax({
        url: "/ATS/GetOSID?id=" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Owner').val(result[0].Owner);
            $('#Package').val(result[0].Package);
            $('#WindowOS').val(result[0].WindowOS);
            $('#MSrelease').val(result[0].MSrelease);
            $('#ITrelease').val(result[0].ITrelease);
            $('#Input_date').val(result[0].Package);

            $('#myModal').modal('show');
            $('#btnUpdate').show();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record  
function UpdateOS() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        Owner: $('#Owner').val(),
        Package: $('#Package').val(),
        WindowOS: $('#WindowOS').val(),
        MSrelease: $('#MSrelease').val(),
        ITrelease: $('#ITrelease').val(),
        Input_date: $('#Input_date').val(),
    };
    $.ajax({
        url: "/ATS/UpdateOS",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#id').val("");
            $('#Owner').val("");
            $('#Package').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function DeleteWindowOS(id) {
    var ans = confirm("Are you sure delete ?" + id);
    if (ans) {
        $.ajax({
            url: "/ATS/DeleteOS?id=" + id,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {

    $('#Owner').val("");
    $('#Package').val("");
    $('#WindowOS').val("");
    $('#MSrelease').val("");
    $('#ITrelease').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#Owner').css('border-color', 'lightgrey');
    $('#Package').css('border-color', 'lightgrey');
    $('#WindowOS').css('border-color', 'lightgrey');
    $('#MSrelease').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#Owner').val().trim() == "") {
        $('#Owner').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Owner').css('border-color', 'lightgrey');
    }

    if ($('#Package').val().trim() == "") {
        $('#Package').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Package').css('border-color', 'lightgrey');
    }
    return isValid;
}  
