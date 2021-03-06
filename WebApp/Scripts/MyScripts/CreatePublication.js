﻿$(function () {
    var id = $("#Id").val();
    $("#proptype").change(function () {
        $("#propcontent").empty();
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
            url: "/Home/ContentForFlat/" + id,
            dataType: 'html',
            success: function (data) {
                $('#propcontent').html(data);
            }
        });
    }
    function Room() {
        $.ajax({
            url: "/Home/ContentForRoom/" + id,
            dataType: 'html',
            success: function (data) {
                $('#propcontent').html(data);
            }
        });
    }
    function House() {
        $.ajax({
            url: "/Home/ContentForHouse/" + id,
            dataType: 'html',
            success: function (data) {
                $('#propcontent').html(data);
            }
        });
    }
    function Property() {
        $.ajax({
            url: "/Home/ContentForProperty/" + id,
            dataType: 'html',
            success: function (data) {
                $('#propcontent').html(data);
            }
        });
    }

})