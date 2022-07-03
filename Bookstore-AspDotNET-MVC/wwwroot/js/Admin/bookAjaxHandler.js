
//==========SHOW POPUP ADD/EDIT FORM=================
showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        },
        error: function (er) {
            console.log(er.responseText);
        }

    })
}

//==========CALL AJAX ADD/EDIT FORM=================
ajaxPostBook = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res === "Thêm thành công!!") {
                    window.location.reload();
                } else {
                    $("#form-modal .modal-body").html(res);
                }
                
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


//==========CALL AJAX DELETE BOOK=================
ajaxDeleteBook = (form,cap) => {
    const id=form.action.substring(form.action.lastIndexOf("=")+1)
    if (confirm(`bạn có mún xóa ${cap} có id =${id} không ?`))
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
                alert(er.responseText)
                //console.log(er.responseText);
            }

        })
    } catch (e) {
        console.log(e)
    }
    return false
}