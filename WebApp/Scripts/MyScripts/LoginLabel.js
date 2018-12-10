$(function () {

    $.ajax({
        async: false,
        url: "/Account/IsAuthenticated",
        dataType: 'json',
        success: function (data) {
            if (data) {
                $.ajax({
                    async: false,
                    url: "/Account/ReturnUsername",
                    dataType: 'json',
                    success: function (data) {
                        $('#name').html(data);
                    }
                });
            }
            else {
                $('#name').html('<a href="/Account/Login">Логин</a>');
            }
        }
    });


    $('#navgrid > ul > li').hover(function (event) {
            if ($('#name').html() != '<a href="/Account/Login">Логин</a>') {
                $(this).children("ul").slideDown();
            }
        },
        function () {
            if ($('#name').html() != '<a href="/Account/Login">Логин</a>') {
                $(this).children("ul").clearQueue();
                $(this).children("ul").stop();
                $(this).children("ul").slideUp();
            }
        }
    );

    $('#name').hover(
        function () {
            $(this).css("color", "red");
            $(this).css("cursor", "pointer");
        },
        function () {
            $(this).css("color", "dimgrey");
            $(this).css("cursor", "default");
        });
})