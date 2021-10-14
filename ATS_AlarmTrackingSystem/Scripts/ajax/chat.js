
$('#prime').click(function () {
    LoadMessage();
    toggleFab();
    $("#chat_fullscreen").animate({ scrollTop: $("#chat_fullscreen")[0].scrollHeight*10 }, 1000);
});


//Toggle chat and links
function toggleFab() {
    $('.prime').toggleClass('zmdi-comment-outline');
    $('.prime').toggleClass('zmdi-close');
    $('.prime').toggleClass('is-active');
    $('.prime').toggleClass('is-visible');
    $('#prime').toggleClass('is-float');
    $('.chat').toggleClass('is-visible');
    $('.fab').toggleClass('is-visible');

    //$([document.documentElement, document.body]).animate({
    //    scrollTop: $("#scroll_end").offset().top
    //}, 500);
}

$('#chat_fullscreen_loader').click(function (e) {
    $('.fullscreen').toggleClass('zmdi-window-maximize');
    $('.fullscreen').toggleClass('zmdi-window-restore');
    $('.chat').toggleClass('chat_fullscreen');
    $('.fab').toggleClass('is-hide');
    $('.header_img').toggleClass('change_img');
    $('.img_container').toggleClass('change_img');
    $('.chat_header').toggleClass('chat_header2');
    $('.fab_field').toggleClass('fab_field2');
    $('.chat_converse').toggleClass('chat_converse2');
});

function LoadMessage() {
    $.ajax({
        url: "/api/getLimitMessage",
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            var owner_emp = $('#owner_emp').html();
            $.each(result, function (key, item) {
                if (item.ROW2 == owner_emp) {
                    html += '<div class="form-group"><span class="chat_msg_item chat_msg_item_user">' + item.CONTENTS + '</span ><p class="date_massage2">' + item.DATESEND + '</p></div>';
                } else {
                    var user;
                    if (item.ROW2 == null) {
                        user = 'Anonymous';
                    } else {
                        user = item.ROW2;
                    }
                    html += '<div class="form-group"><span class="chat_msg_item chat_msg_item_admin"><div class="chat_avatar"><img src="../Assets/Image/chatbot2.png" /></div > <div class="chat-content" onclick="replyIP(\'' + item.ROW2 + '\')">';
                    html += '<h6 class="name_massage">' + user + '</h6>';
                    html += '<span class="conten_message">' + item.CONTENTS + '</span></div></span >';
                    html += '<p class="date_massage1">' + item.DATESEND + '</p></div>';
                }
            });
            //html += '<div class="form-group"><span class="chat_msg_item chat_msg_item_user" id="message_send">...</span><p class="date_massage2">2021/07/24 14:22:05</p></div>';
            //html += '<p id="scroll_end" class="status"></p>';
            $('#chat_fullscreen').html(html);
            $("#chat_fullscreen").animate({ scrollTop: $("#chat_fullscreen")[0].scrollHeight*10 }, 1000);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
                                                                                                                                                               
        }
    });
    setTimeout(LoadMessage, 100000);

}

var reply='';
function replyIP(ID_MESSAGE) {
    reply = ID_MESSAGE;
    if (reply != '') {
        $('#id_reply').text(reply);
        $('#div_reply').show();
    }
}

function remove_reply() {
    $('#id_reply').text('');
    $('#div_reply').hide();
}

function SendMessage() {
    var mess = $('#chatSend').val();
    var owner_emp = $('#owner_emp').html();

    if (owner_emp == "") {
        alert('Please login. Using send message');
        return false;
    }

    var empObj = {
       "KEY": "vmOffiiEN5ngggQuty3y2knI9472jFUyqg9SK4KF80BUwV1pDX8W70PDtj1BBpv0/dMYCSVuWwiFkivzfkMduOUevWu2cfZJtAToZQLhvjrQLnEkqbhLhaLshtnJ2bXC",
       "GROUP_NAME": "SFCBN3_Issue",
       "CONTENTS": mess,
       "USERSEND": owner_emp,
       "REPLY": reply,
     };

    if (mess != '') {
        
        $.ajax({
            url: "/apizalo/SendMessage",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#message_send').text(mess);

                var today = new Date();
                var dd = String(today.getDate()).padStart(2, '0');
                var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                var yyyy = today.getFullYear();

                today = mm + '/' + dd + '/' + yyyy;

                html = '<div class="form-group"><span class="chat_msg_item chat_msg_item_user">' + mess + '</span ><p class="date_massage2">' + today + '</p></div>';
                $("#chat_fullscreen").append(html);

                $("#chat_fullscreen").animate({ scrollTop: $("#chat_fullscreen")[0].scrollHeight*10 }, 1000);
                $('#chatSend').val('');
                remove_reply();
                LoadMessage();
                //alert(result);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });

    }
}

