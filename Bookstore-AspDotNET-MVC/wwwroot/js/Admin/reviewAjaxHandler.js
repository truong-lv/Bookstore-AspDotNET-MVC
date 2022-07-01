//==========CALL AJAX DELETE BOOK=================
ajaxDeleteReview = form => {
    const id = form.action.substring(form.action.lastIndexOf("=") + 1)
    if (confirm(`bạn có mún xóa bình luận này không ?`))
        try {
            $.ajax({
                type: "POST",
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    alert(res)
                    window.location.reload();

                },
                error: function (er) {
                    //console.log(er.responseText);
                }

            })
        } catch (e) {
            console.log(e)
        }
    return false
}