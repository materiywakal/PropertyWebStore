$(function () {
    var publicationsCount = '';
    $.ajax({
        url: "/Home/ReturnPublicationsCount",
        dataType: 'json',
        success: function (data) {
            publicationsCount = data;
            var output = 'На сайте зарегистрировано ' + publicationsCount + ' ';
            switch (publicationsCount.toString()[publicationsCount.toString().length - 1]) {
                case '1': output += 'объявление.'; break;
                case '2': output += 'объявления.'; break;
                case '3': output += 'объявления.'; break;
                case '4': output += 'объявления.'; break;
                default: output += 'объявлений.'; break;
            }
            $('#indexPageText').text(output);
        }
    }); 
});