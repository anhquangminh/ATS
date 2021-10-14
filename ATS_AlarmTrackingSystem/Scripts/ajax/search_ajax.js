                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
function ClearHistory() {
    var backlen = history.length;
    history.go(-backlen);
    window.location.href = loggedOutPageUrl
}

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

function SearchInfor() {
    var reg = $('#search').val();
    //$("#load").css("display", "none");
    //$("#load").css("display", "block");

    $('#121212').show();
    
    //DEPARTMENT_NAME
    $.ajax({
        type: "Get",
        url: 'https://appvn6.foxconn.com.vn:1236/api/hr/getEmpObj',
        contentType: "application/json; charset=utf-8",
        data: { id: reg },
        success: function (data) {
            if (data != "") {
                var html = '';
                html += '<tr>';
                html += '<td>' + data.USER_NAME + '</td>';
                html += '<td>' + data.DEPARTMENT_NAME + '</td>';
                html += '<td style="text-align:center;" ><a href="#" data-dismiss="modal" onclick="return SetInput(\'' + data.USER_ID + '\',\'' + data.USER_NAME + '\',\'' + data.DEPARTMENT_NAME + '\',\'' + data.NOTES_ID + '\')"><i class="fas fa-edit"></i></a></td>';
                html += '</tr>';
                $('#121212').hide();
                $('#tbody').html(html);
                $('#dresult').show();
            } else {
                var html = '';
                html += '<tr>';
                html += '<td> EMP does not exist !</td>';
                html += '</tr>';
                $('#121212').hide();
                $('#tbody').html(html);
                $('#dresult').show();
            }
            
        },
        error: function (error) {
            alert(error);
        },
    });
}

function SetInput(USER_ID, USER_NAME, DEPARTMENT_NAME, NOTES_ID) {
    $('#emp').val(USER_ID);
    $('#name').val(USER_NAME);
    $('#depart').val(DEPARTMENT_NAME);
    $('#email').val(NOTES_ID);
    return false;
}

function UpdateGA() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#id').val(),
        mathe: $('#mathe').val(),
        hoten: $('#hoten').val(),
        hotentq: $('#hotentq').val(),
        phapnhan: $('#phapnhan').val(),
        khuxuong: $('#khuxuong').val(),
        bu: $('#bu').val(),
        cft: $('#cft').val(),
        toanha: $('#toanha').val(),
        bophan: $('#bophan').val(),
        ngayvaoxuong: $('#ngayvaoxuong').val(),
        phancap: $('#phancap').val(),
        capbac: $('#capbac').val(),
        gioitinh: $('#gioitinh').val(),
        machiphi: $('#machiphi').val(),
        ngaysinh: $('#ngaysinh').val(),
        cmt: $('#cmt').val(),
        ngayvaoKTX: $('#ngayvaoKTX').val(),
        ngayraKTX: $('#ngayraKTX').val(),
        quequan: $('#quequan').val(),
        toaKTX: $('#toaKTX').val(),
        sophong: $('#sophong').val(),
        sogiuong: $('#sogiuong').val(),
        ghichu: $('#ghichu').val(),
    };
    $.ajax({
        url: "/ATS/UpdateGA",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            location.reload();
            $('#myModal').modal('hide');
            $('#id').val("");
            $('#mathe').val("");
            $('#hoten').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function DeleteGA(id) {
    var ans = confirm("Ban co chac mon xoa ?");
    if (ans) {
        $.ajax({
            url: "/ATS/DeleteGA?id=" + id,
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