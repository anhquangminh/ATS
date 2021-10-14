var emp, timestart, mealType, aroundTime, foundData;
var itemSizeHeightSmall = 30;
var itemSizeWidthSmall = 115;
var itemSizeHeightBig = 30;
var itemSizeWidthBig = 115;
var itemSpace = 0;

function validatesearch() {
    var isValid = true;
    if ($('#emp').val().trim() == "") {
        $('#emp').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#emp').css('border-color', 'lightgrey');
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

function SearchTime() {
    //Check input
    var res = validatesearch();
    if (res == false) {
        return false;
    }
    //Get search conditions
    var emp = $('#emp').val();
    var timestart = $('#timestart').val();
    var timeend = $('#timeend').val();
    var selectaround = $('#selectaround').val();

    if (new Date(timestart) > new Date(timeend)) {
        alert("Time Start start time must be greater than end time !")
        return false;
    }
    $('#loadingsearch').show();
    $.ajax({
        url: "/ATS/getTstartTend?emp=" + emp + '&timestart=' + timestart + '&timeend=' + timeend + '&selectaround=' + selectaround,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                var html = '<tr class="table-primary"><td>Emp No/ Mã thẻ</td><td>Name/ Tên</td><td>Position/ Vị trí </td><td>Time/ thời gian</td><td></td></tr>';
                $.each(data, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.empno + '</td>';
                    html += '<td>' + item.empname + '</td>';
                    html += '<td>' + item.position + '</td>';
                    html += '<td>' + item.time + '</td>';
                    html += '<td>'+item.ff+'</td>';
                    html += '</tr>';
                });
                $('#tbodySearch').html(html);
                $('#downloadsearch').show();
                $('#loadingsearch').hide();
            } else {
                $('#loadingsearch').hide();
                $('#nodata').show();
                $('#tbodySearch').remove();
            }

        },
        error: function (error) {
            $('#loadingsearch').hide();
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

