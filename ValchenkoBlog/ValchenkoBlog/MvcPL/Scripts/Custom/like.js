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
                $('#like' + id).text(result);               
            },
            async: true,
        });
    });
});