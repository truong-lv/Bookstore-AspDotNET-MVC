//==========ORDER CONFIRM HANDLER=================
orderConfirmHandler = form => {
    const id = form.action.substring(form.action.lastIndexOf("=") + 1)
    if (confirm(`bạn có mún duyệt đơn hàng có id =${id} không ?`)) {
        return true;
    }
    return false
}

//==========ORDER CANCLE HANDLER=================
orderCancleHandler = form => {
    const id = form.action.substring(form.action.lastIndexOf("=") + 1)
    if (confirm(`bạn có mún hủy đơn hàng có id =${id} không ?`)) {
        return true;
    }
    return false
}


getOrderDetail = form => {
    $.ajax({
        type: 'GET',
        url: form.action,
        success: function (res) {
            var row = document.querySelector(".container-order-detail .card");
            row.innerHTML = res;
            $("#form-modal").modal('show');
        }
    })
    return false;
}