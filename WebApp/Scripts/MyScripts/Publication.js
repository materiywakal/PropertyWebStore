$(function () {

    switch ($("#proptype").val()) {
        case '1': Flat($("#publId").val()); break;
        case '2': Room($("#publId").val()); break;
        case '3': House($("#publId").val()); break;
        case '4': Property($("#publId").val()); break;
    }

    var currentImage = 1;
    var imagesAmount = 1;
    var publicationId = $("#publId").val();
    $.ajax({
        url: "/Home/ChangeAmountOfViews",
        type: "POST",
        data: "publicationId=" + publicationId,
        success: function (data) {

        }
    });
    $.ajax({
        url: "/Home/ImagesAmount",
        async: false,
        data: "publicationId=" + publicationId,
        dataType: 'json',
        success: function (data) {
            imagesAmount = data;
        }
    });
    $(".left").hover(
        function () {
            if (currentImage > 1) {
                $(this).children().attr("src", "/Images/Resources/rightarrowred.png");
                $(this).css("cursor", "pointer");
            }
            else {
                $(this).css("cursor", "arrow");
            }
        },
        function () {
            $(this).children().attr("src", "/Images/Resources/rightarrowgrey.png");
            $(this).css("cursor", "default");
        }
    );
    $(".right").hover(
        function () {
            if (currentImage < imagesAmount) {
                $(this).children().attr("src", "/Images/Resources/rightarrowred.png");
                $(this).css("cursor", "pointer");
            }
            else {
                $(this).css("cursor", "arrow");
            }
        },
        function () {
            $(this).children().attr("src", "/Images/Resources/rightarrowgrey.png");
            $(this).css("cursor", "default");
        }
    );
    $(".left").click(function () {
        var bufcount = currentImage - 1;
        if (bufcount > 1) {
            $(this).children().attr("src", "/Images/Resources/rightarrowred.png");
            $(this).css("cursor", "pointer");
        }
        else {
            $(this).children().attr("src", "/Images/Resources/rightarrowgrey.png");
            $(this).css("cursor", "arrow");
        }
        $.ajax({
            url: "/Home/ReturnImagePathAndNumber",
            data: "currentImage=" + currentImage + "&publicationId=" + publicationId + "&isLeft=true",
            dataType: 'json',
            success: function (data) {
                if (currentImage != data[1]) {
                    $("#image").attr("src", data[0]);
                    currentImage = data[1];
                }
            }
        });
    });
    $(".right").click(function () {
        var bufcount = currentImage + 1;
        if (bufcount < imagesAmount) {
            $(this).children().attr("src", "/Images/Resources/rightarrowred.png");
            $(this).css("cursor", "pointer");
        }
        else {
            $(this).children().attr("src", "/Images/Resources/rightarrowgrey.png");
            $(this).css("cursor", "arrow");
        }
        $.ajax({
            url: "/Home/ReturnImagePathAndNumber",
            data: "currentImage=" + currentImage + "&publicationId=" + publicationId + "&isLeft=false",
            dataType: 'json',
            success: function (data) {
                if (currentImage != data[1]) {
                    $("#image").attr("src", data[0]);
                    currentImage = data[1];
                }
            }
        });
    });

    function Flat(value) {
        $.ajax({
            url: "/Home/PublicationFlat",
            data: "id=" + value,
            dataType: 'html',
            success: function (data) {
                $('#properties').html(data);
            }
        });
    }
    function Room(value) {
        $.ajax({
            url: "/Home/PublicationRoom",
            data: "id=" + value,
            dataType: 'html',
            success: function (data) {
                $('#properties').html(data);
            }
        });
    }
    function House(value) {
        $.ajax({
            url: "/Home/PublicationHouse",
            data: "id=" + value,
            dataType: 'html',
            success: function (data) {
                $('#properties').html(data);
            }
        });
    }
    function Property(value) {
        $.ajax({
            url: "/Home/PublicationProperty",
            data: "id=" + value,
            dataType: 'html',
            success: function (data) {
                $('#properties').html(data);
            }
        });
    }
})