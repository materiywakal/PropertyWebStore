$(function () {

    $.ajax({
        async: false,
        url: "/Account/IsAdmin",
        dataType: 'json',
        success: function (data) {
            if (data) {
                $("#labels").append('<li><span id="adminlabel">Администрирование</span><ul class="submenu"><li><a href="/Admin/UnconfirmedPublications">Проверка объявлений</a></li><li><a href="/Admin/Statistics">Статистика</a></li></ul></li>');
            }
        }
    });


    $('#adminlabel').parent().hover(function (event) {
            $(this).children("ul").slideDown();
        },
        function () {
            $(this).children("ul").clearQueue();
            $(this).children("ul").stop();
            $(this).children("ul").slideUp();
        }
    );

    $('#adminlabel').hover(
        function () {
            $(this).css("color", "red");
            $(this).css("cursor", "pointer");
        },
        function () {
            $(this).css("color", "dimgrey");
            $(this).css("cursor", "default");
        });
})