$(document).ready(function () {
    $('#submit').click(function () {

        var postId = $("input[name=postId]").val();
        var text = $("input[name=commentText]").val();

        $.ajax({
            type: 'POST',
            url: "/Post/AddComment",
            data: {
                'postId': postId,
                'text' : text
            },
            dataType: 'json',
            success: function (result) {
                //alert(JSON.parse(result));
            },
            async: true,
        });
    });
});