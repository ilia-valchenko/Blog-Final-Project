$(document).ready(function () {

    $("#comment-form-id").submit(function (event) {

            event.preventDefault();

            $.ajax({
                type: 'POST',
                url: "/Post/AddCommentViaAjax",
                data: {
                    'postId': $('#postid').val(),
                    'text': $('#text').val()
                },
                dataType: 'json',
                success: function (result) {
            
                    $("#commentwrapper").append('<div class="comment">' +
                        '<div class="avatar-wrapper"><img src="/Content/Images/Avatars/default.png" alt="default_user_avatar"></div>' +
                            '<div class="comment-area">' + 
                                '<p><strong>' + result.User.Nickname + '</strong><span>' + result.PublishDate + '</span></p>' +
                                '<p>' + result.Text + '</p>' + 
                                '<form method="post" id="comment-delete-form" action="/Post/DeleteComment">' + 
                                    '<input type="hidden" id="commentid" name="id" ' + 'value=' + result.Id + '>' +
                                    '<input type="submit" value="Delete">' +
                                '</form>' +
                            '</div>' +
                        '</div>');
                    
                    $("#number_of_comments").replaceWith(parseInt($("#number_of_comments").text()) + 1);

                },
                async: true,
            });

    });

    $("#comment-delete-form").submit(function (event) {
        
        event.preventDefault();
        var id = $(this).find("#commentid").val();

        $.ajax({
            type: 'POST',
            url: "/Post/DeleteCommentViaAjax",
            data: {
                'id': id
            },
            dataType: 'json',
            success: function (result) {
                $("#comment" + id).remove();
                $("#number_of_comments").replaceWith(parseInt($("#number_of_comments").text()) - 1);
            },
            async: true,
        });

    });

});


 