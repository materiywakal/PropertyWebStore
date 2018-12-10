$(function () {
    ymaps.ready(function () {
        var yandexMap = new ymaps.Map('yandexMap', {
            center: [53.9, 27.56],
            zoom: 11.5,
            behaviors: ['default','scrollZoom']
        });
        var placemarkCollection = new ymaps.Clusterer({
            clusterDisableClickZoom: false,
            clusterIcons: [{
                href: 'https://api-maps.yandex.ru/2.0/images/9c0ea6ee005905d8f43f915be190cc3d.png',
                size: [34, 34],
                offset: [-17, -17]
            }]
        });
        $.ajax({
            url: '/Home/ReturnPublications',
            dataType: 'json',
            success: function (data) {
                data.forEach(function (item, i, data) {
                    var coords = item.Coordinates.split(',');
                    var myPlacemark = new ymaps.Placemark([coords[0], coords[1]], {}, { preset: 'twirl#redIcon' });
                    myPlacemark.events.add('click', function () {
                        window.location.href = '/Home/Publication?id=' + item.Id;
                    });
                    placemarkCollection.add(myPlacemark);
                });
            }
        });
        yandexMap.geoObjects.add(placemarkCollection);

        yandexMap.controls.add('zoomControl', { right: '20px', top: '35px' });
        yandexMap.controls.add(new ymaps.control.TypeSelector());
    });
});