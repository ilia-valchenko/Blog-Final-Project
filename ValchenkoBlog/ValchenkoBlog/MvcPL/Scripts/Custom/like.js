$(document).ready(function () {  
    $('.like-button').click(function () {

        event.preventDefault();
        var id = $(this).attr('id');

        $.ajax({
            type: 'POST',
            url: "/Post/Like",
            data: { 'id': id },
            dataType: 'json',
            success: function (result) 
            {
                if (result)
                {
                    $('#like' + id).html(parseInt($('#like' + id).text()) - 1);
                    $('#like' + id).parent().removeClass("active");
                }
                else
                {
                    $('#like' + id).html(parseInt($('#like' + id).text()) + 1);
                    $('#like' + id).parent().addClass("active");
                }
            },
            async: true,
        });
    });
});