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

                    var file = base64js.fromByteArray(jQuery.makeArray(result.User.Avatar));

                    $("#commentwrapper").append('<div class="comment" id="comment' + result.Id + '">' +
                        '<div class="avatar-wrapper">' +
                            '<img src="data:image/png;base64,' + file + '" alt="' + result.User.Nickname + '_avatar_image' + '">' +
                        '</div>' + 
                            '<div class="comment-area">' + 
                                '<p><strong>' + result.User.Nickname + '</strong><span>' + result.PublishDate + '</span></p>' +
                                '<p>' + result.Text + '</p>' + 
                                '<form method="post" id="comment-delete-form" action="/Post/DeleteComment">' + 
                                    '<input type="hidden" id="commentid" name="id" ' + 'value=' + result.Id + '>' +
                                    '<input type="submit" class="dynamic_element_submit" value="Delete">' +
                                '</form>' +
                            '</div>' +
                        '</div>');

                    $("#number_of_comments").html(parseInt($("#number_of_comments").text()) + 1);
                    $("#text").val('');

                    $("#comment-delete-form").submit(function (event) {

                        event.preventDefault();
                        alert("Click by submit button");

                        var id = $(this).find("#commentid").val();

                        $.ajax({
                            type: 'POST',
                            url: "/Post/DeleteCommentViaAjax",
                            data: {
                                'id': id
                            },
                            dataType: 'json',
                            success: function (result) {
                                alert("Successfully deleted!");
                                alert($("#comment" + id));
                                $("#comment" + id).remove();
                                alert("After deleting");
                                $("#number_of_comments").html(parseInt($("#number_of_comments").text()) - 1);
                            },
                            async: true,
                        });

                    });

                    
                },
                async: true,
            });

    });

    // Delete comment

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
                $("#number_of_comments").html(parseInt($("#number_of_comments").text()) - 1);
            },
            async: true,
        });

    });

});



 