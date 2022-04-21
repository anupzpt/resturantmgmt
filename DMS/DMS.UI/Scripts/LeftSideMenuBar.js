function htmlbodyHeightUpdate() {
    var height3 = $(window).height()
    var height1 = $('.nav').height() + 50
    height2 = $('.main').height()
    if (height2 > height3) {
        $('html').height(Math.max(height1, height3, height2) + 10);
        $('body').height(Math.max(height1, height3, height2) + 10);
    }
    else {
        $('html').height(Math.max(height1, height3, height2));
        $('body').height(Math.max(height1, height3, height2));
    }

}
$(document).ready(function () {
    htmlbodyHeightUpdate()
    $(window).resize(function () {
        htmlbodyHeightUpdate()
    });
    $(window).scroll(function () {
        height2 = $('.main').height()
        htmlbodyHeightUpdate()
    });
});

function HoverOn(x) {
    $('#mainBody').css('margin-left', 320 + 'px');
}
function HoverOff(x) {
    $('#mainBody').css('margin-left', 60 + 'px');
}
//$('li').click(function () {
//    kah = $(this).attr('data-target');
//    if (kah == null) {
//        alert("::jhbjhhg");
//    }
//});
