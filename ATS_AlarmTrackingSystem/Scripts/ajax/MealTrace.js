var emp, timestart, mealType, aroundTime, foundData;
var itemSizeHeightSmall = 30;
var itemSizeWidthSmall = 115;
var itemSizeHeightBig = 30;
var itemSizeWidthBig = 115;
var itemSpace = 0;
var scaleVal = 10;
function validatesearch() {
    var isValid = true;
    if ($('#emp7day').val().trim() == "") {
        $('#emp7day').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#emp7day').css('border-color', 'lightgrey');
    }

    if ($('#timestart').val().trim() == "") {
        $('#timestart').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#timestart').css('border-color', 'lightgrey');
    }

    return isValid;
}
function searchNear() {
    //Check input
    var res = validatesearch();
    if (res == false) {
        return false;
    }
    //Get search conditions
    emp = $('#emp7day').val();
    timestart = $('#timestart').val();
    mealType = $('#selectmeal').val();
    aroundTime = $('#selectaround').val();
    foundData = false;
    $('html,body').animate({ scrollTop: $('.result').offset().top });
    //Draw Canteen over view
    DrawCanteenOverview2();
}

function DrawPosition(ctx, x, y, color, itemSizeWidth, itemSizeHeight, text) {
    if (color != 'n/a') {
        ctx.fillStyle = '#757575';
        //ctx.fillStyle = '#000000';
        ctx.fillRect(x, y, itemSizeWidth, itemSizeHeight);
        if (color == 'white') {
            ctx.fillStyle = '#ffffff';
        } else {
            ctx.fillStyle = color;
        }
        ctx.fillRect(x + 2, y + 2, itemSizeWidth - 3, itemSizeHeight - 3);
    } else {
        ctx.fillStyle = '#f5f5f5';
        //ctx.fillStyle = '#000000';
        ctx.fillRect(x + 2, y + 2, itemSizeWidth - 3, itemSizeHeight - 3);
    }
    if (text != undefined) {
        ctx.font = "11px Tahoma";
        if (color != 'white')
            ctx.fillStyle = 'white';
        else
            ctx.fillStyle = '#9e9e9e';
        ctx.fillText(text, x + 4, y + itemSizeHeight / 2 + 3);
    }
}
function GetCanteenOverview(emp, timestart, mealtype) {
    return $.ajax({
        url: "/api/Covid_MapOverView?dateSearch=" + timestart + "&emp=" + emp + "&EatTime=" + mealtype,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json"
    });
}
function DrawCanteenOverview(callback) {
    GetCanteenOverview(emp, timestart, mealType).then((data) => {
        if (data == '') {
            alert('Emp ' + emp + ' không đi ăn vào thời gian đã chọn.');
            foundData = false;
        } else {
            foundData = true;
            var startX = 5, startY = 5;
            var canvas = document.getElementById("mapOver");
            canvas.width = 1000;
            canvas.height = 1000;
            var ctx = canvas.getContext("2d");
            var maxX = 0, maxY = 0;
            data.forEach((item) => {
                startX = item.Position_intX * itemSizeWidthSmall + itemSpace;
                startY = item.Position_intY * itemSizeHeightSmall + itemSpace;
                maxX = startX > maxX ? startX : maxX;
                maxY = startY > maxY ? startY : maxY;
            });
            if (maxX < canvas.scrollWidth) {
                canvas.width = maxX;
            }
            if (maxY < canvas.scrollHeight) {
                canvas.height = maxY;
            }
            data.forEach((item) => {
                startX = (item.Position_intX - 1) * itemSizeWidthSmall + itemSpace;
                startY = (item.Position_intY - 1) * itemSizeHeightSmall + itemSpace;
                //maxX = startX > maxX ? startX : maxX;
                //maxY = startY > maxY ? startY : maxY;
                if (item.Name_Position != 'N/A') {
                    DrawPosition(ctx, startX, startY, item.Color, itemSizeWidthSmall, itemSizeHeightSmall);
                } else {
                    DrawPosition(ctx, startX, startY, 'n/a', itemSizeWidthSmall, itemSizeHeightSmall);
                }
            });

            if (callback !== 'undefinded')
                callback(ctx);
        }
    });
}
function DrawCanteenOverview2(callback) {
    GetCanteenOverview(emp, timestart, mealType).then((data) => {
        if (data == '') {
            $('.result-found-data').addClass('hidden');
            $('.message-area').removeClass('hidden');
            var messageContent = 'Employee ' + emp + ' not have eating scan data in chosen time. &nbsp;&nbsp;&nbsp;&nbsp;Nhân viên ' + emp + ' không đi ăn vào thời gian đã chọn.'
            $('#message').html(messageContent);
            foundData = false;
        } else {
            $('.result-found-data').removeClass('hidden');
            $('.message-area').addClass('hidden');

            var treeRoot = $('#rootEle');
            treeRoot.empty();
            $('#listEmp').empty();

            foundData = true;
            var startX = 5, startY = 5;
            var canvas = document.getElementById("mapOver");
            canvas.width = 10000;
            canvas.height = 10000;
            var ctx = canvas.getContext("2d");
            var maxX = 0, maxY = 0;
            data.forEach((item) => {
                startX = item.Position_intX * itemSizeWidthBig + itemSpace;
                startY = item.Position_intY * itemSizeHeightBig + itemSpace;
                maxX = startX > maxX ? startX : maxX;
                maxY = startY > maxY ? startY : maxY;
            });
            if (maxX < canvas.scrollWidth) {
                canvas.width = maxX;
            }
            if (maxY < canvas.scrollHeight) {
                canvas.height = maxY;
            }
            data.forEach((item) => {
                startX = (item.Position_intX - 1) * itemSizeWidthBig + itemSpace;
                startY = (item.Position_intY - 1) * itemSizeHeightBig + itemSpace;
                //maxX = startX > maxX ? startX : maxX;
                //maxY = startY > maxY ? startY : maxY; 
                if (item.Name_Position != 'N/A') {
                    DrawPosition(ctx, startX, startY, 'white', itemSizeWidthBig, itemSizeHeightBig, item.Name_Position);
                } else {
                    DrawPosition(ctx, startX, startY, 'n/a', itemSizeWidthBig, itemSizeHeightBig);
                }
            });
            scaleVal = 10;
            GetF0Position(emp, timestart, mealType).then((data) => {
                var color;
                var indexed = "";
                var index = "";
                data.forEach((item) => {
                    index = item.Position_intX + "_" + item.Position_intY;
                    if (indexed.indexOf(index) == -1) {
                        indexed += index + ";";
                        startX = (item.Position_intX - 1) * itemSizeWidthBig + itemSpace;
                        startY = (item.Position_intY - 1) * itemSizeHeightBig + itemSpace;
                        if (item.Patient == 'F0') {
                            color = '#ff0000'; //đỏ
                            $('.canvasDiv').scrollTop(startY - 4 * itemSizeHeightBig - itemSpace);
                            $('.canvasDiv').scrollLeft(startX - 4 * itemSizeWidthBig - itemSpace);
                        }
                        else
                            if (item.Patient == 'F1')
                                color = '#f39c12'; //vàng cam
                            else
                                if (item.Patient == 'F2')
                                    color = '#008000'; //xanh
                                else
                                    if (item.Patient == 'F3')
                                        color = '#5f9ea0'; //ngọc bích
                                    else
                                        if (item.Patient == 'F4')
                                            color = 'white'; //trắng

                        if (item.Name_Position != 'N/A') {
                            DrawPosition(ctx, startX, startY, color, itemSizeWidthBig, itemSizeHeightBig, item.Name_Position);
                        } else {
                            DrawPosition(ctx, startX, startY, 'n/a', itemSizeWidthBig, itemSizeHeightBig);
                        }
                    }
                });
            });

            GetTreeView(emp, timestart, mealType, aroundTime).then((treeNodes) => {

                var emp, name, type, parent, parentRoot, pos;
                var count = 0;
                var html = '<tr><td>EMP No</td><td>Name</td><td>Position </td><td> Time </td><td>Range</td></tr>';

                treeNodes.forEach((node) => {
                    emp = node.EmpNo;
                    name = node.EmpName;
                    type = node.F;
                    parent = node.Parent;
                    pos = node.Position;

                    html += '<tr>';
                    html += '<td>' + emp + '</td>';
                    html += '<td>' + name + '</td>';
                    html += '<td>' + pos + '</td>';
                    html += '<td>' + node.Time + '</td>';
                    html += '<td>' + type + '</td>';
                    html += '</tr>';

                    if (type == 'F0' || parent != '') {
                        $('#listEmp').append(type + ": " + emp + " " + name + "\n");
                        if (type == 'F0') {
                            $('<i class="fas fa-user ficon f0 f00" id="r' + emp + '"></i>').appendTo(rootEle);
                            $('<br />').appendTo(rootEle);
                            $('<a>' + emp + '</a>').appendTo(rootEle);
                            $('<br />').appendTo(rootEle);
                            $('<a>' + pos + '</a>').appendTo(rootEle);
                            $('<ul id=' + emp + '></ul>').appendTo(rootEle);
                        } else {
                            parentRoot = $('#' + parent);
                            $('<li><i class="fas fa-user ficon ' + type.toLowerCase() + '"></i><br><a>' + emp + '</a><br><a>' + pos + '</a><ul id=' + emp + '></ul></li>').appendTo(parentRoot);
                        }
                    }
                    count++;
                    if (count == treeNodes.length) {
                        var left = $('.f00')[0].offsetLeft - $('.treenote').width() / 2;
                        $('.treenote').scrollLeft(left);
                        //Remove all null node
                        $('#rootEle').find('ul:not(:has(li))').remove();
                    }
                });
                $('#tbSearch').html(html);
            });


            //Get table list
            $.ajax({
                url: "/ATS/getDataTree?emp=" + emp + "&&timestart=" + timestart + "&&timeend=" + timestart + "&&select=0",
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
                        $('#tbSearch').html(html);
                    }
                },
                error: function (error) {
                    alert(error);
                },
            });
        }
    });
}
function GetF0Position(emp, timestart, mealtype) {
    return $.ajax({
        url: "/api/Finding_patient?dateSearch=" + timestart + "&emp=" + emp + "&EatTime=" + mealtype,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json"
    });
}
function DrawF0Position(context) {
    if (foundData) {
        GetF0Position(emp, timestart, mealType).then((data) => {
            var startX = 5, startY = 5;
            var canvas = document.getElementById("positionView");
            canvas.width = 1000;
            canvas.height = 1000;
            var ctx = canvas.getContext("2d");
            var maxX = 0, maxY = 0;
            var color;
            var minPX = 99999, minPY = 99999, maxPX = 0, maxPY = 0;
            //Find min, max of position x,y
            // minPx, Py - - - - - - maxPx
            //         - - - - - - - -
            //   maxPy   - - - - - - maxPy
            //
            var F0x, F0y, F0Pos;
            data.forEach((item) => {
                if (item.Patient == 'F0') {
                    F0x = item.Position_intX;
                    F0y = item.Position_intY;
                    F0Pos = item.Name_Position;
                    DrawF0InCanteen(context, F0x, F0y, itemSizeWidthSmall, itemSizeHeightSmall);
                }
                if (item.Position_intX < minPX)
                    minPX = item.Position_intX;
                if (maxPX < item.Position_intX)
                    maxPX = item.Position_intX;
                if (item.Position_intY < minPY)
                    minPY = item.Position_intY;
                if (maxPY < item.Position_intY)
                    maxPY = item.Position_intY;
            });

            data.forEach((item) => {
                startX = (item.Position_intX - minPX) * itemSizeWidthBig + itemSpace;
                startY = (item.Position_intY - minPY) * itemSizeHeightBig + itemSpace;
                maxX = startX > maxX ? startX : maxX;
                maxY = startY > maxY ? startY : maxY;
            });
            if (maxX < canvas.scrollWidth) {
                canvas.width = maxX;
            }
            if (maxY < canvas.scrollHeight) {
                canvas.height = maxY;
            }
            var indexed = "";
            var index = "";
            data.forEach((item) => {
                index = item.Position_intX + "_" + item.Position_intY;
                if (indexed.indexOf(index) == -1) {
                    indexed += index + ";";
                    startX = (item.Position_intX - minPX) * itemSizeWidthBig + itemSpace;
                    startY = (item.Position_intY - minPY) * itemSizeHeightBig + itemSpace;
                    //maxX = startX > maxX ? startX : maxX;
                    //maxY = startY > maxY ? startY : maxY;
                    if (item.Patient == 'F0') {
                        color = '#ff0000'; //đỏ
                    }
                    else
                        if (item.Patient == 'F1')
                            color = '#f39c12'; //vàng cam
                        else
                            if (item.Patient == 'F2')
                                color = '#008000'; //xanh
                            else
                                if (item.Patient == 'F3')
                                    color = '#5f9ea0'; //ngọc bích
                                else
                                    if (item.Patient == 'F4')
                                        color = 'white'; //trắng

                    if (item.Name_Position != 'N/A') {
                        DrawPosition(ctx, startX, startY, color, itemSizeWidthBig, itemSizeHeightBig, item.Name_Position);
                    } else {
                        DrawPosition(ctx, startX, startY, 'n/a', itemSizeWidthBig, itemSizeHeightBig);
                    }
                }
            });
            GetRangeLayout(F0Pos, minPX, maxPX, minPY, maxPY).then((dataRange) => {
                dataRange.forEach((item) => {
                    index = item.Position_intX + "_" + item.Position_intY;
                    if (indexed.indexOf(index) == -1) {
                        color = 'white'; //trắng
                        startX = (item.Position_intX - minPX) * itemSizeWidthBig + itemSpace;
                        startY = (item.Position_intY - minPY) * itemSizeHeightBig + itemSpace;
                        if (item.Name_Position != 'N/A') {
                            DrawPosition(ctx, startX, startY, color, itemSizeWidthBig, itemSizeHeightBig, item.Name_Position);
                        } else {
                            DrawPosition(ctx, startX, startY, 'n/a', itemSizeWidthBig, itemSizeHeightBig);
                        }
                    }
                });
            });
        });
    }
}
function DrawF0InCanteen(ctx, x, y, itemSizeWidth, itemSizeHeight) {
    //alert(x + "," + y);
    DrawPosition(ctx, (x - 1) * itemSizeWidth + itemSpace, (y - 1) * itemSizeHeight + itemSpace, '#ff0000', itemSizeWidth, itemSizeHeight)
    var startX = (x - 3 - 1) * itemSizeWidth + itemSpace;
    var startY = (y - 3 - 1) * itemSizeHeight + itemSpace;

    var mark = new Image();
    mark.src = "/Assets/Image/marker.png";
    mark.onload = function () {
        ctx.drawImage(mark, startX - itemSizeWidth / 2 + 3, startY - itemSizeHeight / 2);
    }
}
function GetRangeLayout(pos, minX, maxX, minY, maxY) {
    return $.ajax({
        url: "/api/GetRangeLayout?postition=" + pos + "&minX=" + minX + "&maxX=" + maxX + "&minY=" + minY + "&maxY=" + maxY,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json"
    });
}
function GetTreeView(emp, timestart, mealtype, aroundTime) {
    return $.ajax({
        url: "/api/TreePatient?dateSearch=" + timestart + "&emp=" + emp + "&EatTime=" + mealtype + "&aroundTime=" + aroundTime,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json"
    });
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
$(document).ready(function () {
    $('html,body').animate({ scrollTop: $('.content').offset().top });
})
function setZoom(zoom, el) {

    transformOrigin = [0, 0];
    el = el || instance.getContainer();
    var p = ["webkit", "moz", "ms", "o"],
        s = "scale(" + zoom + ")",
        oString = (transformOrigin[0] * 100) + "% " + (transformOrigin[1] * 100) + "%";

    for (var i = 0; i < p.length; i++) {
        el.style[p[i] + "Transform"] = s;
        el.style[p[i] + "TransformOrigin"] = oString;
    }

    el.style["transform"] = s;
    el.style["transformOrigin"] = oString;

}

//setZoom(5,document.getElementsByClassName('container')[0]);

function showVal() {
    var zoomScale = Number(scaleVal) / 10;
    setZoom(zoomScale, document.getElementById('mapOver'))
}
function zoomOut() {
    if (scaleVal > 1)
        scaleVal--;
    showVal();
}
function zoomIn() {
    if (scaleVal < 10)
        scaleVal++;
    showVal();
}

function nWin() {
    var canvas = document.getElementById('mapOver');
    var base64ImageData = canvas.toDataURL();
    const contentType = 'image/png';

    const byteCharacters = atob(base64ImageData.substr(`data:${contentType};base64,`.length));
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += 1024) {
        const slice = byteCharacters.slice(offset, offset + 1024);

        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);

        byteArrays.push(byteArray);
    }
    const blob = new Blob(byteArrays, { type: contentType });
    const blobUrl = URL.createObjectURL(blob);

    window.open(blobUrl, '_blank');
}