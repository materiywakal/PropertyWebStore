$(function () {
    $("#filterType").val('1');
    Flat();
    $("#filterType").change(function () {
        $("#outerFilter").empty();
        var value = $(this).val();
        switch (value) {
            case '1': Flat(); break;
            case '2': Room(); break;
            case '3': House(); break;
            case '4': Property(); break;
            default: break;
        }
    });

    function Flat() {
        $.ajax({
            url: "/Home/BrowseFilterFlat",
            dataType: 'html',
            success: function (data) {
                $('#outerFilter').html(data);
            }
        });
    }
    function Room() {
        $.ajax({
            url: "/Home/BrowseFilterRoom",
            dataType: 'html',
            success: function (data) {
                $('#outerFilter').html(data);
            }
        });
    }
    function House() {
        $.ajax({
            url: "/Home/BrowseFilterHouse",
            dataType: 'html',
            success: function (data) {
                $('#outerFilter').html(data);
            }
        });
    }
    function Property() {
        $.ajax({
            url: "/Home/BrowseFilterProperty",
            dataType: 'html',
            success: function (data) {
                $('#outerFilter').html(data);
            }
        });
    }
})