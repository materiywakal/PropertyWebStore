
function Delete(id, page, a) {
    var isSelled = confirm("Вы продали недвижимость?");
    var path = "#delete" + id;
    a.href = "/Home/DeletePublication?id=" + id + "&isSelled=" + isSelled + "&page=" + page;
    alert();
}