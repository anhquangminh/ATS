function action(id_ats) {
    var ans = confirm("Are you sure you want receiving and handling ?");
    var owner_emp = $('#owner_emp').text();
    var owner_name = $('#owner_name').text();
    if (ans) {
        $.ajax({
            url: "/api/updateOwner?id_ats=" + id_ats + "&owner_emp=" + owner_emp + "&owner_name=" + owner_name,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var mes = owner_emp + ': processing request ';
                toastr.success(mes);
                loadDataBuilding();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }


    //Swal.fire({
    //    title: 'Are you sure?',
    //    text: "You want receiving and handling: " + id_ats +" ? ",
    //    icon: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Yes, delete it!'
    //}).then((result) => {
    //    if (result.isConfirmed) {
    //        $.ajax({
    //            url: "/api/updateOwner?id_ats=" + id_ats + "&owner_emp=" + owner_emp + "&owner_name=" + owner_name,
    //            type: "GET",
    //            contentType: "application/json;charset=UTF-8",
    //            dataType: "json",
    //            success: function (result) {
    //                Swal.fire({
    //                    text: "Request processed successfully Succes",
    //                    icon: 'success'
    //                });
    //                var mes = owner_emp + ': processing request ';
    //                toastr.success(mes);
    //                loadDataBuilding();
    //            },
    //            error: function (errormessage) {
    //                Swal.fire({
    //                    text: errormessage,
    //                    icon: 'error'
    //                })
    //            }
    //        });
    //    }
    //})


    
}

//start scroll tới table
function goToByScroll(id) {
    $('html,body').animate({
        scrollTop: $("#" + id).offset().top
    }, 'slow');
}

function gotoMore_infor() {
    goToByScroll("more_infor");
}
//end scroll tới table
var status;
var idats;
function getState(id_ats, state) {
    status = state;
    idats = id_ats;
    //alert(idats);
}

function updateState(stateupdate) {
    if (idats != null && status != null) {
        //alert(stateupdate);
        $.ajax({
            url: "/api/updateStatus?id_ats=" + idats + "&status=" + stateupdate + "&execute=update",
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                location.reload();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Load Data in Table when documents is ready

$(document).ready(function () {
    //loadData();
    loadDataOpen();
    loadDataOnGoing();
    loadDataClose();
});

function loadDataOpen() {
    $("#tbodyOpen tr").remove();
    $.ajax({
        url: "/api/getOpen",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var name = '<tr style="text-align:center" class="bg-gradient-primary">';
            var number = '<tr style="text-align:center">';
            $.each(result, function (key, item) {
                name += '<td style="padding:0px;">' + item.Name + '</td>';
                number += '<td style="padding:0px;">' + item.Number + '</td>';
            });
            name += '</tr>';
            number += '</tr>';
            var html = name + number;
            $('#tbodyOpen').append(html);
        },
    });
    setTimeout(loadDataOpen, 30000);
}

function loadDataOnGoing() {
    $("#tbodyOnGoing tr").remove();
    $.ajax({
        url: "/api/getOnGoing",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var name = '<tr style="text-align:center" class="bg-gradient-primary">';
            var number = '<tr style="text-align:center">';
            $.each(result, function (key, item) {
                name += '<td style="padding:0px;">' + item.Name + '</td>';
                number += '<td style="padding:0px;">' + item.Number + '</td>';
            });
            name += '</tr>';
            number += '</tr>';
            var html = name + number;
            $('#tbodyOnGoing').append(html);
        },
    });
    setTimeout(loadDataOnGoing, 30000);
}

function loadDataClose() {
    $("#tbodyClose tr").remove();
    $.ajax({
        url: "/api/getCLose",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var name = '<tr style="text-align:center" class="bg-gradient-primary">';
            var number = '<tr style="text-align:center">';
            $.each(result, function (key, item) {
                name += '<td style="padding:0px;">' + item.Name + '</td>';
                number += '<td style="padding:0px;">' + item.Number + '</td>';
            });
            name += '</tr>';
            number += '</tr>';
            var html = name + number;
            $('#tbodyClose').append(html);
        },
    });
    setTimeout(loadDataClose, 30000);
}

function LoadDataTable() {
    $(document).ready(function () {
        $('#table_message').DataTable({
            initComplete: function () {
                this.api().columns().every(function () {
                    var column = this;
                    var select = $('<select><option value=""></option></select>')
                        .appendTo($(column.footer()).empty())
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex(0
                                ,$(this).val()
                            );
                            column.search(val ? '^' + val + '$' : '', true, false).draw();
                        });

                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    });
                });
            }
        });
    });
    //$('#table_message').dataTable({
    //    destroy: true,
    //});
}
//Load Data function  
function loadData() {
    $("#tbody tr").remove();
    $.ajax({
        url: "/api/getAllData",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                var html = '';
                html += '<tr>';
                html += '<td>' + item.ip_address + '</td>';
                if (item.owner_emp == "") {
                    html += '<td style="text-align:center; align-content: center; margin: auto; height: 50px; position: relative; min-width:50px;"><div class="phonering-alo-phone phonering-alo-green phonering-alo-show" id="phonering-alo-phoneIcon">';
                    html += '<div class="phonering-alo-ph-circle" title="Receiving and handling"></div><div class="phonering-alo-ph-circle-fill" title="Receiving and handling"></div><div class="phonering-alo-ph-img-circle" title="Receiving and handling" onClick="return action(\'' + item.id_ats + '\')"></div></div></td>';
                } else {
                    html += '<td style="text-align:center; align-content: center; margin: auto; height: 80px; position: relative; min-width:80px;">' + item.owner_emp + '</td>';
                }
                if (item.owner_name == "") {
                    html += '<td style="text-align:center"><i style="background-color:aliceblue;font-size: 16px;" class="fas fa-2x fa-sync-alt fa-spin"></i></td>';
                } else {
                    html += '<td style="text-align:center" >' + item.owner_name + '</td>';
                }

                html += '<td>' + item.building + '</td>';
                html += '<td>' + item.team + '</td>';
                html += '<td>' + item.emp_user + '</td>';
                html += '<td>' + item.level_user + '</td>';
                html += '<td>' + item.description + '</td>';
                html += '<td style="text-align:center;color: blue;"  data-toggle="modal" data-target="#statusAction" onclick="getState(\''+item.id_ats+'\',\''+item.status+'\')">' + item.status + '</td>';
                html += '<td>' + item.time_start + '</td>';
                html += '<td>' + item.time_going + '</td>';
                html += '<td>' + item.time_end + '</td>';
                html += '</tr>';
                $('#tbody').append(html);
            });
            $('#table_message').DataTable({
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
                            const string = d;
                            const substring = "class=";
                            if (string.includes(substring) == true) {

                            } else {
                                select.append('<option value="' + d + '">' + d + '</option>');
                            }

                        });
                    });
                },
                "order": [[1, "asc"]]
            });

        },
    });
}


//Add Data Function   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        ip_address: $('#ip_address').val(),
        building: $('#building').val(),
        team: $('#team').val(),
        emp_user: $('#emp_user').val(),
        description: $('#description').val(),
    };
    $.ajax({
        url: "../../api/sendData",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            LoadDataTable();
            $('#myModal').modal('hide');
            alert(result);
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
            loadData();
            $('#myModal').modal('hide');
            $('#emp').val("");
            $('#name_vn').val("");
            $('#building').val("");
            $('#team').val("");
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

    $("#ip_address").prop('disabled', false);
    $('#building').val("");
    $('#team').val("");
    $('#emp_user').val("");
    $('#description').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#ip_address').css('border-color', 'lightgrey');
    $('#building').css('border-color', 'lightgrey');
    $('#team').css('border-color', 'lightgrey');
    $('#emp_user').css('border-color', 'lightgrey');
    $('#description').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#ip_address').val().trim() == "") {
        $('#ip_address').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ip_address').css('border-color', 'lightgrey');
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

    if ($('#emp_user').val().trim() == "") {
        $('#emp_user').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#emp_user').css('border-color', 'lightgrey');
    }
    if ($('#description').val().trim() == "") {
        $('#description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#description').css('border-color', 'lightgrey');
    }
    return isValid;
}  
