
$(document).ready(function () {
    Loading();
});

function Loading() {
    $('#table_canten').DataTable({
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
        }
    });
}

function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#area').val(),
        position: $('#position').val(),
        user_config: $('#f1').val(),
        time_config: $('#f2').val(),
        time_edit: $('#f3').val()
    };
    $.ajax({
        url: "/ATS/addCanten",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            alert(result);
            //$('#area').val("");
            location.reload();
        },
        error: function (errormessage) {
            //$('#area').val("");
            alert(errormessage.responseText);
        }
    });

}

//Function for getting the Data Based   
function GetbyIDCanten(id) {
    $.ajax({
        url: "/ATS/GetIDCanten?id=" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#id').val(result[0].id);
            $('#position1').val(result[0].position);
            $('#type1').val(result[0].type);

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
function UpdateCanten() {
    var res = validate1();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#id').val(),
        position: $('#position1').val(),
        type: $('#type1').val(),
    };
    $.ajax({
        url: "/ATS/UpdateCanten",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#myModal').modal('hide');
            location.reload();
            $('#id').val("");
            $('#position1').val("");
            $('#type1').val("");

            //alert('sua thanh cong');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function DeleteCanten(id) {
    var ans = confirm("Are you sure you want to delete ? " + id);
    if (ans) {
        $.ajax({
            url: "/ATS/DeleteCanten?id=" + id,
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


//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#area').val().trim() == "") {
        $('#area').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#area').css('border-color', 'lightgrey');
    }

    if ($('#position').val().trim() == "") {
        $('#position').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#position').css('border-color', 'lightgrey');
    }

    if ($('#f1').val().trim() == "") {
        $('#f1').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#f1').css('border-color', 'lightgrey');
    }

    //if ($('#f2').val().trim() == "") {
    //    $('#f2').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#f2').css('border-color', 'lightgrey');
    //}
    return isValid;
}

function validate1() {
    var isValid = true;
    if ($('#type1').val().trim() == "") {
        $('#type1').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#type1').css('border-color', 'lightgrey');
    }

    if ($('#position1').val().trim() == "") {
        $('#position1').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#position1').css('border-color', 'lightgrey');
    }
    return isValid;
}

function validatesearch() {
    var isValid = true;
    if ($('#emp7day').val().trim() == "") {
        $('#emp7day').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#type1').css('border-color', 'lightgrey');
    }

    if ($('#timestart').val().trim() == "") {
        $('#timestart').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#timestart').css('border-color', 'lightgrey');
    }

    if ($('#timeend').val().trim() == "") {
        $('#timeend').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#timeend').css('border-color', 'lightgrey');
    }

    return isValid;
}

function SearchInfor() {
    var emp = $('#search').val();
    //$("#load").css("display", "none");
    //$("#load").css("display", "block");

    $('#121212').show();

    //DEPARTMENT_NAME
    $.ajax({
        url: "/ATS/getUserCanten?emp=" + emp,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var html = '';
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.empno + '</td>';
                    html += '<td>' + item.empname + '</td>';
                    html += '<td>' + item.position + '</td>';
                    html += '<td>' + item.time + '</td>';
                    html += '<td style="text-align:center;" ><a href="#" data-dismiss="modal" onclick="return SetInput(\'' + item.empno + '\',\'' + item.position + '\',\'' + item.time + '\')"><i class="fas fa-edit"></i></a></td>';
                    html += '</tr>';
                });
                $('#121212').hide();
                $('#tbody').html(html);
                $('#dresult').show();
            } else {
                var html = '';
                html += '<tr style="color: red;text-align: center;font-weight: bold;">';
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

function SetInput(empno, position, time) {
    $('#empnosearch').val(empno);
    $('#positionsearch').val(position);
    $('#timesearch').val(time);
    return false;
}

function searchF() {
    var emp = $('#empnosearch').val();
    var position = $('#positionsearch').val();
    var time = $('#timesearch').val();
    $('#loading').show();
    //DEPARTMENT_NAME
    $.ajax({
        url: "/ATS/getPositionF?emp=" + emp + "&&position=" + position + "&&time=" + time,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var html = '<tr><td>Emp No</td><td>Name</td><td>Postion </td><td> Date </td><td>F</td></tr>';
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.empno + '</td>';
                    html += '<td>' + item.empname + '</td>';
                    html += '<td>' + item.position + '</td>';
                    html += '<td>' + item.time + '</td>';
                    html += '<td>' + item.ff + '</td>';
                    html += '</tr>';
                });
                $('#loading').hide();
                $('#tbodyResult').html(html);
                $('#result_search').show();
                $('#downloadexcel').show();

            } else {
                var html = '';
                html += '<tr style="color: red;text-align: center;font-weight: bold;" >';
                html += '<td> No result is found  !</td>';
                html += '</tr>';
                $('#loading').hide();
                $('#tbodyResult').html(html);
                $('#result_search').show();
                //$('#downloadexcel').show();
            }

        },
        error: function (error) {
            alert(error);
        },
    });
}

function exportTableToExcel(tableID, filename) {
    var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
    var textRange; var j = 0;
    tab = document.getElementById(tableID);

    for (j = 0; j < tab.rows.length; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");
    tab_text = tab_text.replace(/<img[^>]*>/gi, "");
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, "");

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, filename);
    }
    else
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
    return (sa);
}

function search7day() {

    var res = validatesearch();
    if (res == false) {
        return false;
    }
    var emp = $('#emp7day').val();
    var timestart = $('#timestart').val();
    var timeend = $('#timeend').val();
    var select = $('#selectsearch').val();
    if (new Date(timestart) > new Date(timeend)) {
        alert("Time Start start time must be greater than end time !")
        return false;
    }
    $('#loadingsearch').show();
    //DEPARTMENT_NAME
    $.ajax({
        url: "/ATS/getDataTree?emp=" + emp + "&&timestart=" + timestart + "&&timeend=" + timeend + "&&select=" + select,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var html = '<tr><td>EMP No</td><td>Name</td><td>Position </td><td> Time </td><td>Range</td></tr>';
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.empno + '</td>';
                    html += '<td>' + item.empname + '</td>';
                    html += '<td>' + item.position + '</td>';
                    html += '<td>' + item.time + '</td>';
                    html += '<td>' + item.ff + '</td>';
                    html += '</tr>';
                });
                $('#loadingsearch').hide();
                $('#tbSearch').html(html);
                $('#result_search').show();
                $('#downloadsearch').show();
                $('#note_rearch').hide();
                $('#rootshow').hide();
            } else {
                var html = '';
                html += '<tr style="color: red;text-align: center;font-weight: bold;">';
                html += '<td> No result is found  !</td>';
                html += '</tr>';
                $('#loadingsearch').hide();
                $('#tbSearch').html(html);
                $('#result_search').show();
                //$('#downloadsearch').show();
                $('#result_search').hide();
                $('#note_rearch').hide();
                $('#rootshow').hide();

            }

        },
        error: function (error) {
            alert(error);
        },
    });
    $('#loadingsearch').hide();
}

function searchTree() {
    var res = validatesearch();
    if (res == false) {
        return false;
    }
    var emp = $('#emp7day').val();
    var timestart = $('#timestart').val();
    var timeend = $('#timeend').val();
    var select = $('#selectsearch').val();
    if (new Date(timestart) > new Date(timeend)) {
        alert("Time Start start time must be greater than end time !")
        return false;
    }
    $('#loadingsearch').show();
    $('#rootshow').show();
    var rootEle = $('#rootEle');
    rootEle.empty();
    var listF1 = "", listF2 = "", listF3 = "";
    var listEmp = "";
    /*
    GetF0(emp, timestart, timeend).then(function (data) {
        if (data != "") {
            $('<i class="fas fa-user ficon f0"></i>').appendTo(rootEle);
            $('<br />').appendTo(rootEle);
            $('<a>' + data[0].EmpNo + '</a>').appendTo(rootEle);
            var f1Root = $('<ul></ul>');
            f1Root.appendTo(rootEle);
            data.foreach((item) => {
                alert('a');
            });
            var each1 = new Promise((resolve, reject) => {
                $.each(data, function (key, f0) {
                    GetF1(f0.Pos, f0.Time).then(function (data) {

                        $.each(data, function (key, f1) {
                            var f2Root = $('<ul></ul>');
                            var f1Ele = $('#' + f1.EmpNo);
                            //if (f1Ele.length == 0) {
                            if (listEmp.indexOf(f1.EmpNo) == -1) {
                                listEmp += f1.EmpNo + ";";
                                f1Ele = $('<li id="' + f1.EmpNo + '"></li>');
                                $('<i class="fas fa-user ficon f1"></i>').appendTo(f1Ele);
                                $('<br />').appendTo(f1Ele);
                                $('<a>' + f1.EmpNo + '</a>').appendTo(f1Ele);
                                f2Root.appendTo(f1Ele);
                            } else {
                                f2Root = $(f1Ele).find("ul")[0];
                            }

                            GetF1(f1.Pos, f1.Time).then(function (data) {
                                $.each(data, function (key, f2) {
                                    var f3Root = $('<ul></ul>');
                                    var f2Ele = $('#' + f2.EmpNo);
                                    //if (f2Ele.length == 0) {
                                    if (listEmp.indexOf(f2.EmpNo) == -1) {
                                        listEmp += f2.EmpNo + ";";
                                        f2Ele = $('<li id="' + f2.EmpNo + '"></li>');
                                        $('<i class="fas fa-user ficon f2"></i>').appendTo(f2Ele);
                                        $('<br />').appendTo(f2Ele);
                                        $('<a>' + f2.EmpNo + '</a>').appendTo(f2Ele);
                                        f3Root.appendTo(f2Ele);
                                    } else {
                                        f3Root = $(f2Ele).find("ul")[0];
                                    }

                                    GetF1(f2.Pos, f2.Time).then(function (data) {
                                        $.each(data, function (key, f3) {
                                            var f3Ele = $('#' + f3.EmpNo);
                                            //if (f3Ele.length == 0) {
                                            if (listEmp.indexOf(f3.EmpNo) == -1) {
                                                listEmp += f3.EmpNo + ";";
                                                f3Ele = $('<li id="' + f3.EmpNo + '"></li>');
                                                $('<i class="fas fa-user ficon f3"></i>').appendTo(f3Ele);
                                                $('<br />').appendTo(f3Ele);
                                                $('<a>' + f3.EmpNo + '</a>').appendTo(f3Ele);
                                            }
                                            f3Ele.appendTo(f3Root);
                                        });
                                    });
                                    f2Ele.appendTo(f2Root);
                                });
                            });
                        });
                    });
                });
            });
            each1.then(() => {
                //Remove all null node
                $(rootEle).find('ul:not(:has(li))').remove();
            });

            $('#loadingsearch').hide();
            $('#result_search').hide();
            //$('#root').html(html);
            //$('#result_search').show();
            $('#note_rearch').show();
            $('#downloadsearch').hide();

        } else {
            var html = '';
            html += '<tr style="color: red;text-align: center;font-weight: bold;">';
            html += '<td> No result is found  !</td>';
            html += '</tr>';
            $('#loadingsearch').hide();
            $('#result_search_tree').html(html);
            //$('#result_search').show();
            $('#downloadsearch').hide();
        }
    });*/

    //Get list position of F0
    $.ajax({
        url: "/api/GetF0?emp=" + emp + "&timeStart=" + timestart + "&timeEnd=" + timeend,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != "") {
                $('<i class="fas fa-user ficon f0"></i>').appendTo(rootEle);
                $('<br />').appendTo(rootEle);
                $('<a>' + data[0].EmpNo + '</a>').appendTo(rootEle);
                var f1Root = $('<ul></ul>');
                $.each(data, function (key, f0) {
                    var positon = f0.Pos;
                    var Time = f0.Time;
                    var hasF1 = false;

                    //Get F1
                    $.ajax({
                        url: "/api/getF1?position=" + positon + "&time=" + Time + "",
                        typr: "GET",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (data) {
                            $.each(data, function (key, f1) {
                                hasF1 = true;
                                var f2Root = $('<ul></ul>');
                                var f1Ele = $('#' + f1.EmpNo);
                                //if (f1Ele.length == 0) {
                                if (listF1.indexOf(f1.EmpNo) == -1) {
                                    listF1 += f1.EmpNo + ";";
                                    f1Ele = $('<li id="' + f1.EmpNo + '"></li>');
                                    $('<i class="fas fa-user ficon f1"></i>').appendTo(f1Ele);
                                    $('<br />').appendTo(f1Ele);
                                    $('<a>' + f1.EmpNo + '</a>').appendTo(f1Ele);
                                    f2Root.appendTo(f1Ele);
                                } else {
                                    f2Root = $(f1Ele).find("ul")[0];
                                }

                                var hasF2 = false;
                                //Get F2
                                $.ajax({
                                    url: "/api/getF1?position=" + f1.Pos + "&time=" + f1.Time + "",
                                    typr: "GET",
                                    contentType: "application/json;charset=UTF-8",
                                    dataType: "json",
                                    success: function (data) {
                                        $.each(data, function (key, f2) {
                                            hasF2 = true;
                                            var f3Root = $('<ul></ul>');
                                            var f2Ele = $('#' + f2.EmpNo);
                                            //if (f2Ele.length == 0) {
                                            if (listF2.indexOf(f2.EmpNo) == -1) {
                                                listF2 += f2.EmpNo + ";";
                                                f2Ele = $('<li id="' + f2.EmpNo + '"></li>');
                                                $('<i class="fas fa-user ficon f2"></i>').appendTo(f2Ele);
                                                $('<br />').appendTo(f2Ele);
                                                $('<a>' + f2.EmpNo + '</a>').appendTo(f2Ele);
                                                f3Root.appendTo(f2Ele);
                                            } else {
                                                f3Root = $(f2Ele).find("ul")[0];
                                            }

                                            var hasF3 = false;
                                            //Get F3
                                            $.ajax({
                                                url: "/api/getF1?position=" + f2.Pos + "&time=" + f2.Time + "",
                                                typr: "GET",
                                                contentType: "application/json;charset=UTF-8",
                                                dataType: "json",
                                                success: function (data) {
                                                    $.each(data, function (key, f3) {
                                                        hasF3 = true;
                                                        var f3Ele = $('#' + f3.EmpNo);
                                                        //if (f3Ele.length == 0) {
                                                        if (listF3.indexOf(f3.EmpNo) == -1) {
                                                            listF3 += f3.EmpNo + ";";
                                                            f3Ele = $('<li id="' + f3.EmpNo + '"></li>');
                                                            $('<i class="fas fa-user ficon f3"></i>').appendTo(f3Ele);
                                                            $('<br />').appendTo(f3Ele);
                                                            $('<a>' + f3.EmpNo + '</a>').appendTo(f3Ele);
                                                        }
                                                        f3Ele.appendTo(f3Root);
                                                        //Remove all null node
                                                        $(rootEle).find('ul:not(:has(li))').remove();
                                                    });
                                                    if (!hasF3 && f3Root !== 'undefined' && f3Root.children.length == 0)
                                                        f3Root.remove();
                                                }
                                            });
                                            f2Ele.appendTo(f2Root);
                                        });
                                        if (!hasF2 && f2Root !== 'undefined' && f2Root.children.length == 0)
                                            f2Root.remove();
                                    }

                                });
                                f1Ele.appendTo(f1Root);
                            });
                            if (hasF1 > 0)
                                f1Root.appendTo(rootEle);
                        }
                    });

                });

                $('#loadingsearch').hide();
                $('#result_search').hide();
                //$('#root').html(html);
                //$('#result_search').show();
                $('#note_rearch').show();
                $('#downloadsearch').hide();

            } else {
                var html = '';
                html += '<tr style="color: red;text-align: center;font-weight: bold;">';
                html += '<td> No result is found  !</td>';
                html += '</tr>';
                $('#loadingsearch').hide();
                $('#result_search_tree').html(html);
                //$('#result_search').show();
                $('#downloadsearch').hide();


            }

        },
        error: function (error) {
            alert(error);
        },
    });
    console.log('Say hello');
}

function GetF0(emp, timestart, timeend) {
    return $.ajax({
        url: "/api/GetF0?emp=" + emp + "&timeStart=" + timestart + "&timeEnd=" + timeend,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json"
    });
}
function GetF1(pos, time) {
    return $.ajax({
        url: "/api/getF1?position=" + pos + "&time=" + time + "",
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json"
    });
}
function searchNote(positon, Time) {
    $.ajax({
        url: "/api/GetF1?position=" + positon + "&&time=" + Time,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, obj) {

            })

        }
    })
}



function clearTextBox() {
    $('#emp7day').val("");
    $('#timestart').val("");
    $('#timeend').val("");
}


/*
$(document).ready(function () {
    Loading();
});

function Loading() {
    $('#table_canten').DataTable({
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
        }
    });
}

function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#area').val(),
        position: $('#position').val(),
        user_config: $('#f1').val(),
        time_config: $('#f2').val(),
        time_edit: $('#f3').val()
    };
    $.ajax({
        url: "/ATS/addCanten",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            alert(result);
            //$('#area').val("");
            location.reload();
        },
        error: function (errormessage) {
            //$('#area').val("");
            alert(errormessage.responseText);
        }
    });

}

//Function for getting the Data Based   
function GetbyIDCanten(id) {
    $.ajax({
        url: "/ATS/GetIDCanten?id=" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#id').val(result[0].id);
            $('#position1').val(result[0].position);
            $('#type1').val(result[0].type);

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
function UpdateCanten() {
    var res = validate1();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#id').val(),
        position: $('#position1').val(),
        type: $('#type1').val(),
    };
    $.ajax({
        url: "/ATS/UpdateCanten",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#myModal').modal('hide');
            location.reload();
            $('#id').val("");
            $('#position1').val("");
            $('#type1').val("");

            //alert('sua thanh cong');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function DeleteCanten(id) {
    var ans = confirm("Are you sure you want to delete ? " + id);
    if (ans) {
        $.ajax({
            url: "/ATS/DeleteCanten?id=" + id,
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


//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#area').val().trim() == "") {
        $('#area').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#area').css('border-color', 'lightgrey');
    }

    if ($('#position').val().trim() == "") {
        $('#position').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#position').css('border-color', 'lightgrey');
    }

    if ($('#f1').val().trim() == "") {
        $('#f1').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#f1').css('border-color', 'lightgrey');
    }

    //if ($('#f2').val().trim() == "") {
    //    $('#f2').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#f2').css('border-color', 'lightgrey');
    //}
    return isValid;
}

function validate1() {
    var isValid = true;
    if ($('#type1').val().trim() == "") {
        $('#type1').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#type1').css('border-color', 'lightgrey');
    }

    if ($('#position1').val().trim() == "") {
        $('#position1').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#position1').css('border-color', 'lightgrey');
    }
    return isValid;
}

function validatesearch() {
    var isValid = true;
    if ($('#emp7day').val().trim() == "") {
        $('#emp7day').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#type1').css('border-color', 'lightgrey');
    }

    if ($('#timestart').val().trim() == "") {
        $('#timestart').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#timestart').css('border-color', 'lightgrey');
    }

    if ($('#timeend').val().trim() == "") {
        $('#timeend').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#timeend').css('border-color', 'lightgrey');
    }

    return isValid;
}

function SearchInfor() {
    var emp = $('#search').val();
    //$("#load").css("display", "none");
    //$("#load").css("display", "block");

    $('#121212').show();

    //DEPARTMENT_NAME
    $.ajax({
        url: "/ATS/getUserCanten?emp=" + emp,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var html = '';
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.empno + '</td>';
                    html += '<td>' + item.empname + '</td>';
                    html += '<td>' + item.position + '</td>';
                    html += '<td>' + item.time + '</td>';
                    html += '<td style="text-align:center;" ><a href="#" data-dismiss="modal" onclick="return SetInput(\'' + item.empno + '\',\'' + item.position + '\',\'' + item.time + '\')"><i class="fas fa-edit"></i></a></td>';
                    html += '</tr>';
                });
                $('#121212').hide();
                $('#tbody').html(html);
                $('#dresult').show();
            } else {
                var html = '';
                html += '<tr style="color: red;text-align: center;font-weight: bold;">';
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

function SetInput(empno, position, time) {
    $('#empnosearch').val(empno);
    $('#positionsearch').val(position);
    $('#timesearch').val(time);
    return false;
}

function searchF() {
    var emp = $('#empnosearch').val();
    var position = $('#positionsearch').val();
    var time = $('#timesearch').val();
    $('#loading').show();
    //DEPARTMENT_NAME
    $.ajax({
        url: "/ATS/getPositionF?emp=" + emp + "&&position=" + position + "&&time=" + time,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var html = '<tr><td>Emp No</td><td>Name</td><td>Postion </td><td> Date </td><td>F</td></tr>';
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.empno + '</td>';
                    html += '<td>' + item.empname + '</td>';
                    html += '<td>' + item.position + '</td>';
                    html += '<td>' + item.time + '</td>';
                    html += '<td>' + item.ff + '</td>';
                    html += '</tr>';
                });
                $('#loading').hide();
                $('#tbodyResult').html(html);
                $('#result_search').show();
                $('#downloadexcel').show();

            } else {
                var html = '';
                html += '<tr style="color: red;text-align: center;font-weight: bold;" >';
                html += '<td> No result is found  !</td>';
                html += '</tr>';
                $('#loading').hide();
                $('#tbodyResult').html(html);
                $('#result_search').show();
                //$('#downloadexcel').show();
            }

        },
        error: function (error) {
            alert(error);
        },
    });
}

function exportTableToExcel(tableID, filename) {
    var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
    var textRange; var j = 0;
    tab = document.getElementById(tableID);

    for (j = 0; j < tab.rows.length; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");
    tab_text = tab_text.replace(/<img[^>]*>/gi, "");
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, "");

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, filename);
    }
    else
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
    return (sa);
}

function search7day() {

    var res = validatesearch();
    if (res == false) {
        return false;
    }
    var emp = $('#emp7day').val();
    var timestart = $('#timestart').val();
    var timeend = $('#timeend').val();
    var select = $('#selectsearch').val();
    if (new Date(timestart) > new Date(timeend)) {
        alert("Time Start start time must be greater than end time !")
        return false;
    }
    $('#loadingsearch').show();
    //DEPARTMENT_NAME
    $.ajax({
        url: "/ATS/getDate?emp=" + emp + "&&timestart=" + timestart + "&&timeend=" + timeend + "&&select=" + select,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var html = '<tr><td>EMP No</td><td>Name</td><td>Position </td><td> Time </td><td>Range</td></tr>';
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.empno + '</td>';
                    html += '<td>' + item.empname + '</td>';
                    html += '<td>' + item.position + '</td>';
                    html += '<td>' + item.time + '</td>';
                    html += '<td>' + item.ff + '</td>';
                    html += '</tr>';
                });
                $('#loadingsearch').hide();
                $('#tbSearch').html(html);
                $('#result_search').show();
                $('#downloadsearch').show();
            } else {
                var html = '';
                html += '<tr style="color: red;text-align: center;font-weight: bold;">';
                html += '<td> No result is found  !</td>';
                html += '</tr>';
                $('#loadingsearch').hide();
                $('#tbSearch').html(html);
                $('#result_search').show();
                //$('#downloadsearch').show();

            }

        },
        error: function (error) {
            alert(error);
        },
    });
}

function searchTree() {
    var res = validatesearch();
    if (res == false) {
        return false;
    }
    var emp = $('#emp7day').val();
    var timestart = $('#timestart').val();
    var timeend = $('#timeend').val();
    var select = $('#selectsearch').val();
    if (new Date(timestart) > new Date(timeend)) {
        alert("Time Start start time must be greater than end time !")
        return false;
    }
    $('#loadingsearch').show();
    $.ajax({
        url: "/ATS/getDate?emp=" + emp + "&&timestart=" + timestart + "&&timeend=" + timeend + "&&select=" + select,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var html = '';
                var root = '';
                var f1 = '';
                var f2 = '';
                var f3 = '';
                var rs = '';

                $.each(data, function (key, item) {
                    if (item.ff == 'Range') {
                        root += '<li><i class="fas fa-user ficon f0"></i> <br/><a>' + item.empno + '</a>';
                    } else {
                        rs='<li><i> </i>'
                    }

                    if (item.ff == 'Range 1') {
                        f1 += '<li><i class="fas fa-user ficon f1" ></i><br/><a>' + item.empno + '</a></li>';
                    }

                    if (item.ff == 'Range 2') {
                        f2 += '<li><i class="fas fa-user ficon f1" ></i><br/><a>' + item.empno + '</a></li>';
                    }
                    if (item.ff == 'Range 3') {
                        f3 += '<li><i class="fas fa-user ficon f1" ></i><br/><a>' + item.empno + '</a></li>';
                    }

                });

                if (emp == 'V0939994' && new Date('2021-07-24') <= new Date(timeend))
                {
                    $('#V093994').show();
                }

                $('#loadingsearch').hide();
                $('#result_search').hide();
                //$('#root').html(html);
                //$('#result_search').show();
                $('#note_rearch').show();
                $('#downloadsearch').hide();

            } else {
                var html = '';
                html += '<tr style="color: red;text-align: center;font-weight: bold;">';
                html += '<td> No result is found  !</td>';
                html += '</tr>';
                $('#loadingsearch').hide();
                $('#result_search_tree').html(html);
                //$('#result_search').show();
                $('#downloadsearch').hide();
                $('#V093994').hide();

            }

        },
        error: function (error) {
            alert(error);
        },
    });
}

function clearTextBox() {
    $('#emp7day').val("");
    $('#timestart').val("");
    $('#timeend').val("");
}*/