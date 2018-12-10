$(function () {

    var chat = $.connection.chatHub;
    $('.chat').scrollTop($('.chat').prop('scrollHeight'))

    chat.client.addMessage = function (isMyMessage, message, time) {
        if (isMyMessage) {
            $('.chat').append('<div class="message rightside">' + htmlEncode(message) + '<div class="messageDate">' + time + '</div>' + '</div>');
        }
        else {
            $('.chat').append('<div class="message leftside">' + htmlEncode(message) + '<div class="messageDate">' + time + '</div>' + '</div>');
        }
        $('.chat').animate({ scrollTop: +$('.chat').prop('scrollHeight')}, "slow");
    };


    $('#sendmessage').hover(
        function () {
            $(this).css("color", "red");
            $(this).css("cursor", "pointer");
        },
        function () {
            $(this).css("color", "dimgrey");
            $(this).css("cursor", "default");
        }
    );

    $.connection.hub.start().done(function () {
        $(function () {
            $.ajax({
                async: false,
                url: "/Account/ReturnUserId",
                dataType: 'json',
                success: function (data) {
                    chat.server.connect(data);
                }
            })
        });

        $('#sendmessage').click(function () {
            chat.server.send($('#User1Id').val(), $('#User2Id').val(), $('#message').val());
            $('#message').val('');
        });
        $('#message').keydown(function (e) {
            if (e.keyCode == 13) {
                chat.server.send($('#User1Id').val(), $('#User2Id').val(), $('#message').val());
                $('#message').val('');
            }
        });
    });
});


function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}