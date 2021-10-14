
function ClearHistory() {
    var backlen = history.length;
    history.go(-backlen);
    window.location.href = loggedOutPageUrl
}


//Load Data function  
//function loadData() {
//    $.ajax({
//        url: "/ATS/JsonGA",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",

//        success: function (result) {
//            var html = '';
//            var stt=0;
//            $.each(result, function (key, item) {
//                stt = stt + 1;
//                html += '<tr>';
//                html += '<td style="text-align:center;" ><a href="#" onclick="return GetbyID(\'' + item.id + '\')"><i class="fas fa-edit"></i></a></td>';
//                html += '<td style="text-align:center;"> <a href="#" onclick="DeleteGA(\'' + item.id + '\')"><i class="far fa-trash-alt"></i></a> </td>';
//                html += '<td>' + item.mathe + '</td>';
//                html += '<td>' + item.hoten + '</td>';
//                html += '<td>' + item.hotentq + '</td>';
//                html += '<td>' + item.phapnhan + '</td>';
//                html += '<td>' + item.khuxuong + '</td>';
//                html += '<td>' + item.bu + '</td>';
//                html += '<td>' + item.cft + '</td>';
//                html += '<td>' + item.toanha + '</td>';
//                html += '<td>' + item.bophan + '</td>';
//                html += '<td>' + item.ngayvaoxuong + '</td>';
//                html += '<td>' + item.phancap + '</td>';
//                html += '<td>' + item.capbac + '</td>';
//                html += '<td>' + item.gioitinh + '</td>';
//                html += '<td>' + item.machiphi + '</td>';
//                html += '<td>' + item.ngaysinh + '</td>';
//                html += '<td>' + item.cmt + '</td>';
//                html += '<td>' + item.ngayvaoKTX + '</td>';
//                html += '<td>' + item.ngayraKTX + '</td>';
//                html += '<td>' + item.quequan + '</td>';
//                html += '<td>' + item.toaKTX + '</td>';
//                html += '<td>' + item.sophong + '</td>';
//                html += '<td>' + item.sogiuong + '</td>';
//                html += '<td>' + item.ghichu + '</td>';
//                html += '<td>' + item.nguoitailen + '</td>';
//                html += '<td>' + item.thoigiantai + '</td>';
//                html += '</tr>';
//            });
//            $('#tbody').html(html);

//            $('#table_ga').DataTable({
//                initComplete: function () {
//                    this.api().columns().every(function () {
//                        var column = this;
//                        var select = $('<select><option value=""></option></select>')
//                            .appendTo($(column.footer()).empty())
//                            .on('change', function () {
//                                var val = $.fn.dataTable.util.escapeRegex(
//                                    $(this).val()
//                                );
//                                column
//                                    .search(val ? '^' + val + '$' : '', true, false)
//                                    .draw();
//                            });

//                        column.data().unique().sort().each(function (d, j) {
//                            select.append('<option value="' + d + '">' + d + '</option>')
//                        });
//                    });
//                }
//            });

//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });

//    var table = $('#table_ga').DataTable({
//        ajax: "data.json"
//    });

//    table.ajax.url('newData.json').load();
//}

//Add Data Function   

function Add() {
    $('#myModalLabel').text('Add new infor owner');
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#id').val(),
        mathe: $('#mathe').val(),
        hoten: $('#hoten').val(),
    };
    $.ajax({
        url: "/ATS/AddUserHR",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            location.reload();
            $('#myModal').modal('hide');
            alert(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based   
function GetbyIDHR(id) {
    $.ajax({
        url: "/ATS/GetbyIDHR?id=" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#id').val(result[0].id);
            $('#mathe').val(result[0].mathe);
            $('#hoten').val(result[0].hoten);
            $('#ip').val(result[0].mathe);

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
function UpdateHR() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#id').val(),
        mathe: $('#mathe').val(),
        hoten: $('#hoten').val(),
        ip: $('#ip').val(),
    };
    $.ajax({
        url: "/ATS/UpdateHR",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#myModal').modal('hide');
            location.reload();
            $('#id').val("");
            $('#mathe').val("");
            $('#hoten').val("");
            $('#ip').val("");
           
            alert('sua thanh cong');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function DeleteHR(id, mathe) {
    var ans = confirm("Ban co chac mon xoa ma the ? " + mathe);
    if (ans) {
        $.ajax({
            url: "/ATS/DeleteHR?id="+id+"&mathe="+ mathe,
            type: "GET",
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

//Function for clearing the textboxes  
function clearTextBox() {
    $('#myModalLabel').text('thêm thông tin 1 người');
    $('#mathe').val("");
    $('#hoten').val("");


    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#mathe').css('border-color', 'lightgrey');
    $('#hoten').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#mathe').val().trim() == "") {
        $('#mathe').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#mathe').css('border-color', 'lightgrey');
    }

    if ($('#hoten').val().trim() == "") {
        $('#hoten').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#hoten').css('border-color', 'lightgrey');
    }
    return isValid;
}  
