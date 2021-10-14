//lock unlock CMC

function SearchInfor() {
    var res = validateSearch();
    if (res == false) {
        return false;
    }
    var emp = $('#search').val();
    $('#loading').show();
    //DEPARTMENT_NAME
    $.ajax({
        type: "Get",
        url: '/api/SearchCMC',
        contentType: "application/json; charset=utf-8",
        data: { emp: emp },
        success: function (data) {
            if (data != "") {
                var html = '';
                html += '<tr style="background-color:dodgerblue;color:white;"><td> EMP No </td>';
                html += '<td>Code</td>';
                html += '<td>Message</td></tr>';
                /*html += '<td></td>'*/

                html += '<tr>';
                html += '<tr>';
                html += '<td>' + emp + '</td>';
                html += '<td>' + data.Code + '</td>';
                html += '<td>' + data.Message + '</td>';
                //html += '<td style="text-align:center;" ><a href="#" data-dismiss="modal" ><i class="fas fa-edit"></i></a></td>';
                html += '</tr>';
                $('#loading').hide();
                $('#tbodySearch').html(html);
                $('#dresult').show();
            } else {
                var html = '';
                html += '<tr>';
                html += '<td> EMP does not exist !</td>';
                html += '</tr>';
                $('#loading').hide();
                $('#tbodySearch').html(html);
                $('#dresult').show();
            }

        },
        error: function (error) {
            alert(error);
        },
    });
}

function UnlockEmp() {
    var emp = $('#emps').val();
    var reason = $('#reason').val();
    var res = validateUnlock();
    if (res == false) {
        return false;
    }
    $.ajax({
        type: "Get",
        url: '/api/UnlockCMC',
        contentType: "application/json; charset=utf-8",
        data: { emp: emp, reason: reason },
        success: function (data) {
            if (data != "") {
                var html="";
                html= '<tr style="background-color:dodgerblue;color:white;"><td>STT</td><td> EMP No </td>';
                html += '<td>Reason</td><td>Code</td>';
                html += '<td>Message</td></tr>';
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.STT + '</td>';
                    html += '<td>' + item.Emp + '</td>';
                    html += '<td>' + item.Reason + '</td>';
                    html += '<td>' + item.Code + '</td>';
                    html += '<td>' + item.Message + '</td>';
                    html += '</tr>';
                });
                //html += '<td style="text-align:center;" ><a href="#" data-dismiss="modal" ><i class="fas fa-edit"></i></a></td>';
                $('#loading').hide();
                $('#tbodySearch').html(html);
                $('#dresult').show();
            } else {
                var html = '';
                html += '<tr>';
                html += '<td> EMP does not exist !</td>';
                html += '</tr>';
                $('#loading').hide();
                $('#tbodySearch').html(html);
                $('#dresult').show();
            }

        },
        error: function (error) {
            alert(error);
        },
    });
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
function validateSearch() {
    var isValid = true;
    if ($('#search').val().trim() == "") {
        $('#search').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#search').css('border-color', 'lightgrey');
    }
    return isValid;
}

function validateUnlock() {
    var isValid = true;
    if ($('#emps').val().trim() == "") {
        $('#emps').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#emps').css('border-color', 'lightgrey');
    }
    if ($('#reason').val().trim() == "") {
        $('#reason').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#reason').css('border-color', 'lightgrey');
    }
    return isValid;
}

function action() {
    document.getElementById('btnUnlockEmp').style.display = 'none';
    document.getElementById('btnSearchEmp').style.display = 'block';
    document.getElementById('divSearch').style.display = 'block';
    document.getElementById('divUnlock').style.display = 'none';
    document.getElementById('dresult').style.display = 'block';
    document.getElementById('dresult1').style.display = 'none';
    document.getElementById("btnSearch").classList.add('btn-danger');
    document.getElementById("btnSearch").classList.remove('btn-default');
    document.getElementById("btnUnlock").classList.add('btn-default');
    document.getElementById("btnUnlock").classList.remove('btn-danger');

}

function action2() {
    document.getElementById('dresult').style.display = 'none';
    document.getElementById('dresult1').style.display = 'block';
    document.getElementById('btnSearchEmp').style.display = 'none';
    document.getElementById('btnUnlockEmp').style.display = 'block';
    document.getElementById('divSearch').style.display = 'none';
    document.getElementById('divUnlock').style.display = 'block';
    document.getElementById("btnUnlock").classList.add('btn-danger');
    document.getElementById("btnUnlock").classList.remove('btn-default');
    document.getElementById("btnSearch").classList.add('btn-default');
    document.getElementById("btnSearch").classList.remove('btn-danger');

}

//lock unlock CMC


function loadData() {
    $.ajax({
        url: "/ATS/ListIsolation",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            var stt = 0;
            $.each(result, function (key, item) {
                stt = stt + 1;
                html += '<tr>';
                //html += '<td style="text-align:center;" ><a href="#" onclick="return GetbyID(\'' + item.id + '\')"><i class="fas fa-edit"></i></a></td>';
                //html += '<td style="text-align:center;"> <a href="#" onclick="DeleteIsolation(\'' + item.id + '\')"><i class="far fa-trash-alt"></i></a> </td>';
                html += '<td>' + item.NhaXuong + '</td>';
                html += '<td>' + item.PhapNhan + '</td>';
                html += '<td>' + item.ToaXuong + '</td>';
                html += '<td>' + item.BU + '</td>';
                html += '<td>' + item.CFT + '</td>';
                html += '<td>' + item.BoPhan + '</td>';
                html += '<td>' + item.Chuyen + '</td>';
                html += '<td>' + item.CaLV + '</td>';
                html += '<td>' + item.IDL + '</td>';
                html += '<td>' + item.MaNhanVien + '</td>';
                html += '<td>' + item.TenTV + '</td>';
                html += '<td>' + item.TenTQ + '</td>';
                html += '<td>' + item.GioiTinh + '</td>';
                html += '<td>' + item.NgaySinh + '</td>';
                html += '<td>' + item.DienThoai + '</td>';
                html += '<td>' + item.PhuongTien + '</td>';
                html += '<td>' + item.DCTamTru + '</td>';
                html += '<td>' + item.DCThuongTru + '</td>';
                html += '<td>' + item.LoaiHinhCNV + '</td>';
                html += '<td>' + item.NgayLVCuoi + '</td>';
                html += '<td>' + item.NgayTiepXuc + '</td>';
                html += '<td>' + item.CachLyNgay + '</td>';
                html += '<td>' + item.DuKienKetThucCL + '</td>';
                html += '<td>' + item.ChonDoTX + '</td>';
                html += '<td>' + item.NguyenNhanCT + '</td>';
                html += '<td>' + item.LoaiHinhCL + '</td>';
                html += '<td>' + item.PhuongThucCL + '</td>';
                html += '<td>' + item.NgayXN + '</td>';
                html += '<td>' + item.DiaDiemXN + '</td>';
                html += '<td>' + item.TiemVaccien + '</td>';
                html += '<td>' + item.ThoiGianTiem + '</td>';
                html += '<td>' + item.DiaDiemTiem + '</td>';
                html += '<td>' + item.DaKetThucCL + '</td>';
                html += '<td>' + item.TinhTrangCL + '</td>';
                html += '<td>' + item.NgayKTCL + '</td>';
                html += '<td>' + item.TinhTrang + '</td>';
                html += '<td>' + item.NguoiTaiLen + '</td>';
                html += '<td>' + item.NgayTaiLen + '</td>';
                html += '</tr>';
            });
            $('#tbody').html(html);

            $('#table_Isolation').DataTable({
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
}

//delete
function DeleteIsolation(id) {
    var ans = confirm("Are you sure you want to delete ? ");
    if (ans) {
        $.ajax({
            url: "/ATS/DeleteIsolation?id=" + id,
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

function GetbyID(id) {
    $.ajax({
        url: "/ATS/GetbyIDIsolation?id=" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#id').val(result[0].id);
            $('#NhaXuong').val(result[0].NhaXuong);
            $('#PhapNhan').val(result[0].PhapNhan);
            $('#ToaXuong').val(result[0].ToaXuong);
            $('#BU').val(result[0].BU);
            $('#CFT').val(result[0].CFT);
            $('#bu').val(result[0].bu);
            $('#cft').val(result[0].cft);
            $('#BoPhan').val(result[0].BoPhan);
            $('#Chuyen').val(result[0].Chuyen);
            $('#CaLV').val(result[0].CaLV);
            $('#IDL').val(result[0].IDL);
            $('#MaNhanVien').val(result[0].MaNhanVien);
            $('#TenTV').val(result[0].TenTV);

            $('#TenTQ').val(result[0].TenTQ);
            $('#GioiTinh').val(result[0].GioiTinh);
            $('#NgaySinh').val(result[0].NgaySinh);
            $('#DienThoai').val(result[0].DienThoai);
            $('#PhuongTien').val(result[0].PhuongTien);
            $('#DCTamTru').val(result[0].DCTamTru);
            $('#DCThuongTru').val(result[0].DCThuongTru);
            $('#LoaiHinhCNV').val(result[0].LoaiHinhCNV);
            $('#NgayLVCuoi').val(result[0].NgayLVCuoi);
            $('#NgayTiepXuc').val(result[0].NgayTiepXuc);
            $('#CachLyNgay').val(result[0].CachLyNgay);

            $('#DuKienKetThucCL').val(result[0].DuKienKetThucCL);
            $('#ChonDoTX').val(result[0].ChonDoTX);
            $('#NguyenNhanCT').val(result[0].NguyenNhanCT);
            $('#LoaiHinhCL').val(result[0].LoaiHinhCL);
            $('#PhuongThucCL').val(result[0].PhuongThucCL);
            $('#NgayXN').val(result[0].NgayXN);
            $('#DiaDiemXN').val(result[0].DiaDiemXN);
            $('#TiemVaccien').val(result[0].TiemVaccien);
            $('#ThoiGianTiem').val(result[0].ThoiGianTiem);
            $('#DiaDiemTiem').val(result[0].DiaDiemTiem);
            $('#DaKetThucCL').val(result[0].DaKetThucCL);
            $('#TinhTrangCL').val(result[0].TinhTrangCL);
            $('#NgayKTCL').val(result[0].NgayKTCL);
            $('#TinhTrang').val(result[0].TinhTrang);


            $('#myModal').modal('show');
            $('#btnUpdate').show();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#MaNhanVien').val().trim() == "") {
        $('#MaNhanVien').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MaNhanVien').css('border-color', 'lightgrey');
    }

    if ($('#TenTV').val().trim() == "") {
        $('#TenTV').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TenTV').css('border-color', 'lightgrey');
    }
    return isValid;
}
function UpdateIsolation() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#id').val(),
        NhaXuong: $('#NhaXuong').val(),
        PhapNhan: $('#PhapNhan').val(),
        ToaXuong: $('#ToaXuong').val(),
        BU: $('#BU').val(),
        CFT: $('#CFT').val(),
        bu: $('#bu').val(),
        cft: $('#cft').val(),
        BoPhan: $('#BoPhan').val(),
        Chuyen: $('#Chuyen').val(),
        CaLV: $('#CaLV').val(),
        IDL: $('#IDL').val(),
        MaNhanVien: $('#MaNhanVien').val(),
        TenTV: $('#TenTV').val(),

        TenTQ: $('#TenTQ').val(),
        GioiTinh: $('#GioiTinh').val(),
        NgaySinh: $('#NgaySinh').val(),
        DienThoai: $('#DienThoai').val(),
        PhuongTien: $('#PhuongTien').val(),
        DCTamTru: $('#DCTamTru').val(),
        DCThuongTru: $('#DCThuongTru').val(),
        LoaiHinhCNV: $('#LoaiHinhCNV').val(),
        NgayLVCuoi: $('#NgayLVCuoi').val(),
        NgayTiepXuc: $('#NgayTiepXuc').val(),
        CachLyNgay: $('#CachLyNgay').val(),

        DuKienKetThucCL: $('#DuKienKetThucCL').val(),
        ChonDoTX: $('#ChonDoTX').val(),
        NguyenNhanCT: $('#NguyenNhanCT').val(),
        LoaiHinhCL: $('#LoaiHinhCL').val(),
        PhuongThucCL: $('#PhuongThucCL').val(),
        NgayXN: $('#NgayXN').val(),
        DiaDiemXN: $('#DiaDiemXN').val(),
        TiemVaccien: $('#TiemVaccien').val(),
        ThoiGianTiem: $('#ThoiGianTiem').val(),
        DiaDiemTiem: $('#DiaDiemTiem').val(),
        DaKetThucCL: $('#DaKetThucCL').val(),
        TinhTrangCL: $('#TinhTrangCL').val(),
        NgayKTCL: $('#NgayKTCL').val(),
        TinhTrang: $('#TinhTrang').val()
    };
    $.ajax({
        url: "/ATS/UpdateIsolation",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#myModal').modal('hide');
            $('#id').val("");
            $('#mathe').val("");
            $('#hoten').val("");
            loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}