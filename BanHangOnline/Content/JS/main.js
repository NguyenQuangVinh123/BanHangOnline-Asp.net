$.ajax({
    url: "localhost:55902/Home/Index/GioHangPartial",
    type: 'GET',
    success: function (data) {
        $('#cart').html($(data).find('#cart').html());
    }
});
$.ajax({
    url: "localhost:55902/Home/Index/GioHangPartial",
    type: 'POST',
    success: function (data) {
        $('#cart').html($(data).find('#cart').html());
    }
});