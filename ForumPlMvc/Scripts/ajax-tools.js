var clickedPage;
var clickedItem;

$(document).ready(function () {
    $("#ajax").css('display', 'inline');

    $(".pagination li a").click(savePage);
});

function savePage() {
    clickedItem = this;
    clickedPage = $(this).text();
}

function onAjaxComplete(request, status) {
    var activeItem = $("li.active");
    var page = parseInt(activeItem.text());
    var clone;

    if (clickedPage == "«") {
        clone = activeItem.prev().clone();
        activeItem.prev().replaceWith('<li class="active"><span>' + (page - 1) + '</span></li>');
    } else if (clickedPage == "»") {
        clone = activeItem.next().clone();
        activeItem.next().replaceWith('<li class="active"><span>' + (page + 1) + '</span></li>');
    } else {
        clone = $(clickedItem).parent().clone();
        $(clickedItem).parent().replaceWith('<li class="active"><span>' + clickedPage + '</span></li>');
    }

    clone.children("a").text(page);
    var href = clone.children("a").prop('href');
    href = href.substring(0, href.indexOf('page='));
    clone.children("a").prop('href', href + 'page=' + page);

    activeItem.replaceWith(clone);

    if ($("li.active").children().text() == "1") {
        $("li.active").prev().replaceWith('<li class="disabled"><span>«</span></li>');
    } else {
        var clone = $("li.active").prev().clone();
        clone.children("a").text("«");
        $(".pagination li").first().replaceWith(clone);
    }

    var lastPage = $(".pagination li").last().prev();
    if (lastPage.hasClass('active')) {
        lastPage.next().replaceWith('<li class="disabled"><span>»</span></li>');
    } else {
        var clone = $("li.active").next().clone();
        clone.children("a").text("»");
        $(".pagination li").last().replaceWith(clone);
    }
    $(".pagination li a").click(savePage);
}
