$(function () {
    // for sidebar menu
    var url = window.location;
    $('ul.nav-sidebar a').filter(function () {
        if (this.href) {
            return this.href == url || url.href.indexOf(this.href) == 0;
        }
    }).addClass('active');
});
