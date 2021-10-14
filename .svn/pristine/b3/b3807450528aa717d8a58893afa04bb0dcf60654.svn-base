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

function LoadDataTable() {
    $('#table_owner').DataTable({
        initComplete: function () {
            this.api().columns().every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });
    $('#table_owner').dataTable({
        destroy: true,
    });
}
//Load Data function  
function loadData() {
    $.ajax({
        url: "/ATS/ListOwner",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (result) {
            var html = '';
            var stt=0;
            $.each(result, function (key, item) {
                stt = stt + 1;
                html += '<tr>';
                html += '<td>' + stt + '</td>';
                html += '<td>' + item.emp + '</td>';
                html += '<td>' + item.name_cn + '</td>';
                html += '<td>' + item.name_vn + '</td>';
                html += '<td>' + item.building + '</td>';
                html += '<td>' + item.team + '</td>';
                html += '<td>' + item.trusted + '</td>';
                html += '<td style="text-align:center;" ><a href="#" onclick="return GetbyOwnerID(\'' + item.emp + '\')"><i class="fas fa-edit"></i></a></td>';
                html += '<td style="text-align:center;"> <a href="#" onclick="Delele(\'' + item.emp + '\')"><i class="far fa-trash-alt"></i></a> </td>';
                html += '</tr>';
            });
            $('#tbody').html(html);
            $('#table_owner').DataTable({
                initComplete: function () {
                    this.api().columns().every(function () {
                        var column = this;
                        var select = $('<select><option value=""></option></select>')
                            .appendTo($(column.footer()).empty())
                            .on('change', function () {
                                var val = $.fn.dataTable.util.escapeRegex(
                                    $(this).val()
                                );

                                column
                                    .search(val ? '^' + val + '$' : '', true, false)
                                    .draw();
                            });

                        column.data().unique().sort().each(function (d, j) {
                            select.append('<option value="' + d + '">' + d + '</option>')
                        });
                    });
                }
            });
           
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    var table = $('#table_owner').DataTable({
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
            alert(result);
            $('#myModal').modal('hide');
            loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon Employee ID  
function GetbyOwnerID(emp) {
    $('#myModalLabel').text('Update infor owner');
    $('#emp').css('border-color', 'lightgrey',);
    $("#emp").prop('disabled', true);
    $('#name_cn').css('border-color', 'lightgrey');
    $('#building').css('border-color', 'lightgrey');
    $('#team').css('border-color', 'lightgrey');
    $.ajax({
        url: "/ATS/GetbyOwnerID?emp=" + emp,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#emp').val(result[0].emp);
            $('#name_vn').val(result[0].name_vn);
            $('#building').val(result[0].building);
            $('#team').val(result[0].team);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record  
function UpdateOwner() {
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
        url: "/ATS/UpdateOwner",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#myModal').modal('hide');
            $('#emp').val("");
            $('#name_vn').val("");
            $('#building').val("");
            $('#team').val("");
            loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function Delele(emp) {
    var ans = confirm("Are you sure you want to delete this emp ?");
    if (ans) {
        $.ajax({
            url: "/ATS/Delete?emp=" + emp,
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
    $('#myModalLabel').text('Add new infor owner');
    $("#emp").prop('disabled', false);
    $('#emp').val("");
    $('#name_vn').val("");
    $('#building').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#emp').css('border-color', 'lightgrey');
    $('#name_vn').css('border-color', 'lightgrey');
    $('#building').css('border-color', 'lightgrey');
    $('#team').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#emp').val().trim() == "") {
        $('#emp').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#emp').css('border-color', 'lightgrey');
    }

    if ($('#name_vn').val().trim() == "") {
        $('#name_vn').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#name_vn').css('border-color', 'lightgrey');
    }

    if ($('#building').val().trim() == "") {
        $('#building').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#building').css('border-color', 'lightgrey');
    }

    if ($('#team').val().trim() == "") {
        $('#team').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#team').css('border-color', 'lightgrey');
    }
    return isValid;
}  
