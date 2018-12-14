$(function () {
    ymaps.ready(function () {
        var yandexMap = new ymaps.Map('yandexMap', {
            center: [53.9, 27.56],
            zoom: 11.5,
            behaviors: ['default', 'scrollZoom']
        });
        var tryCords = $("#Coordinates").val();
        var myPlacemark;
        if (tryCords!=null) {
            var arr = tryCords.split(',');
            myPlacemark = new ymaps.Placemark([arr[0], arr[1]], {}, { preset: 'twirl#redIcon' });
        }
        else {
            myPlacemark = new ymaps.Placemark([53.9, 27.56], {}, { preset: 'twirl#redIcon' });
        }
        yandexMap.geoObjects.add(myPlacemark);
        yandexMap.controls.add('zoomControl', { right: '20px', top: '35px' });
        yandexMap.controls.add(new ymaps.control.TypeSelector());
        yandexMap.events.add('click', function (e) {
            var coords = e.get('coords');
            yandexMap.geoObjects.remove(myPlacemark);
            myPlacemark = new ymaps.Placemark([coords[0].toPrecision(6), coords[1].toPrecision(6)], {}, { preset: 'twirl#redIcon' });
            yandexMap.geoObjects.add(myPlacemark);
            $.ajax({
                url: "https://geocode-maps.yandex.ru/1.x",
                data: "?apikey=271bbac4-94da-43a8-b5a4-90fa6966b4d0&format=json&geocode=" + coords[1].toPrecision(6) + "," + coords[0].toPrecision(6),
                dataType: 'jsonp',
                success: function (data) {
                    var result = data.response.GeoObjectCollection.featureMember[0].GeoObject.metaDataProperty.GeocoderMetaData.text;
                    $("#Address").val(result);
                    $("#Coordinates").val(coords[0].toPrecision(6) + "," + coords[1].toPrecision(6));
                }
            });
        });
    });
});