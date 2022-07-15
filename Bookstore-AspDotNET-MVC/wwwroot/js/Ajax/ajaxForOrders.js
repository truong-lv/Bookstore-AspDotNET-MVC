var form = $('#__AjaxAntiForgeryForm');
var token = $('input[name="__RequestVerificationToken"]', form).val();
$(".btn-info-order").click(function() {


	var orderId = $(this).attr("data-order-id");
	$.ajax({

		type: "GET",
		url: "/OrderApi/OrderDetail",
		data: {
			orderId: orderId

		},
		success: function(value) {
			$(".cart-container").html(value);
			$(".cart-container").css("display", "block");

		}, error: () => {
			console.log('Error');


		}
	})


})

$(".btn-confirm").click(function () {

	var r = confirm("Xác nhận đã nhận đơn hàng ");
	if (r == true) {
		var orderId = $(this).attr("data-order-id");
		var thisBlock = $(this);
		var orderStatus = $(this).parent("div").parent("div").find(".order-status");
		var btnCancle = $(this).parent("div").parent("div").parent("div").find(".btn-cancle-order");
		$.ajax({

			type: "POST",
			url: "/OrderApi/OrderInvited",
			data: {
				__RequestVerificationToken: token,
				orderId: orderId

			},
			success: function () {
				$(thisBlock).hide();
				btnCancle.hide();
				$(orderStatus).replaceWith('<p class="order-status c-6 text-green"><i class="fas fa-hourglass-end"></i> Trạng thái: Đã giao</p>')
			}, error: () => {
				console.log('Error');


			}
		})

	} else {
		alert("Thao tác bị hủy");
	}


})

$(".btn-cancle-order").click(function() {

	var r = confirm("Bạn có chắc chắn muốn hủy đơn hàng? ");
	if (r == true) {
		var orderId = $(this).attr("data-order-id");
		var thisBlock = $(this);
		var orderStatus = $(this).parent("div").parent("div").find(".order-status");
		var btnConfirm = $(this).parent("div").parent("div").find(".btn-confirm");
	$.ajax({

		type: "POST",
		url: "/OrderApi/CancleOrder",
		data: {
			__RequestVerificationToken: token,
			orderId: orderId

		},
		success: function() {
			$(thisBlock).hide();
			btnConfirm.hide();
			$(orderStatus).replaceWith('<p class="order-status c-6 text-danger"><i class="fas fa-check-circle"></i> Trạng thái: Đã hủy</p>')
		}, error: () => {
			console.log('Error');


		}
	})

	} else {
		alert("Thao tác bị hủy"); 
	}
	

})
//$("#btn-order-again").click(function() {

//	alert("bạn đặt hàng lại à");

//})












