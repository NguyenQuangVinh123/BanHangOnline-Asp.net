/**
 * 
 */

$(document).ready(function ($) {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 150) {
            $(".header-bottom").addClass('fixNav')
        } else {
            $(".header-bottom").removeClass('fixNav')
        }
    }
    )
})

$(document).ready(function () {
    $(".beta-select").click(function () {
        $(".beta-dropdown").toggle();
    });
});



       