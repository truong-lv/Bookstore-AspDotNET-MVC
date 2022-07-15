var form = $('#__AjaxAntiForgeryForm');
var token = $('input[name="__RequestVerificationToken"]', form).val();
$("#btn-add-to-cart").click(function () {
	

	var id = $("#idBook").val(); 
	var quantity = $("#quantity").val(); 

	if (quantity === "") { alert("Số lượng không được để trống !! "); return; }

	if (quantity < $("#quantity").attr("min") || quantity > $("#quantity").attr("max")) {
		alert(`Số lượng không hợp lệ !! \nĐặt tối thiểu 1, tối đa ${$("#quantity").attr("max")}`);
		return;
    }

	$.ajax({
		type: "POST",
		url: `/CartApi/AddOrUpdate?bookId=${id}&quantity=${quantity}`,
		data: {
			__RequestVerificationToken: token
		},
		success: function (value) {
			$("#quantity").val('');
			alert(value); 
		},error: (er) => {
			console.log(er);
	}

	})
})

$(".quantity-book").change(function () {
	var block = $(this).closest(".cart-item");
	var bookId = $(this).closest(".cart-item").find(".book").attr("data-idBook");
	var quantity = $(this).val();


	if (quantity === "") { alert("Số lượng không được để trống !! "); return; }

	if (Number(quantity) < Number($(this).attr("min")) || Number(quantity) > Number($(this).attr("max"))) {
		$(this).val(1)
		alert(`Số lượng không hợp lệ !! \nĐặt tối thiểu 1, tối đa ${$(this).attr("max")}`);
		return;
	}


	$.ajax({
		url: "/CartApi/AddOrUpdate",
		type: "POST",
		data: {
			__RequestVerificationToken: token,
			bookId: bookId,
			quantity: quantity
		},
		success: function (value) {
				var price = block.find(".book-price").attr("data-book-price");
				var priceOfItem = price * quantity;
				var priceOfItemFormat = priceOfItem.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace(".00", "");
				var totalPriceFormat = parseInt(value).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace(".00", "");
			block.find(".item-price")
				.replaceWith('<p class="item-price">Thành tiền: <span style="font-weight: bold">' + priceOfItemFormat+'đ</span></p >');
				$("#total-price").replaceWith('<p style="font-size:25px" id= "total-price">Tổng tiền <span style="font-weight:bold; color:red">' + totalPriceFormat + ' đ </span> </p>');
		}
	})
})




$(".btn-delete-item").click(function () {

	var block = $(this).closest(".cart-item");
	var bookId = $(this).closest(".cart-item").find(".book").attr("data-idBook");

	$.ajax({

		type: "POST",
		url: "/CartApi/Delete",
		data: {
			__RequestVerificationToken: token,
			bookId: bookId

		},
		success: function (value) {

			if (value == "false") {
				alert("Xóa thất bại");

			} else {
				if (value == 0) $("#btn-buy").hide();

				block.remove();
				var totalPriceFormat = parseInt(value).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace(".00", "");
				$("#total-price").replaceWith('<p style="font-size:25px" id= "total-price">Tổng tiền <span style="font-weight:bold; color:red">' + totalPriceFormat + ' đ </span> </p>');
			}
		}, error: () => {
			console.log('Error');
		}

	})
})
$("#btn-buy").click(function () {
	$("#block-info-buy").css("display", "block");
})


$("#dropdown-provinces").change(function () {


	var provinceId = $(this).val();
	$.ajax({

		type: "POST",
		url: "/AddressAPI/getDisTrict",
		data: {
			provinceId: provinceId

		},
		success: function (value) {
			let strHtml = value.map((ele, index) => (`<option value=${ele.districtId}>${ele.districtPrefix} ${ele.districtName}</option>`))
			$("#dropdown-districts").html(strHtml.join(''));

		}, error: () => {
			console.log('Error');
		}

	})

})


$("#dropdown-districts").change(function () {

	var districtId = $(this).val();
	$.ajax({

		type: "POST",
		url: "/AddressAPI/getWard",
		data: {
			districtId: districtId

		},
		success: function (value) {
			let strHtml = value.map((ele, index) => (`<option value=${ele.wardId}>${ele.wardPrefix} ${ele.wardName}</option>`))
			$("#dropdown-villages").html(strHtml.join(''));

		}, error: () => {
			console.log('Error');
		}

	})

})


$('#btn-verify-buys').click(function () {
	console.log("click")

	let checkValidate = true;
	const fullname = $('#fullname').val();
	const phone = $('#phone').val();
	const address = $('#address').val();
	const village = $('#dropdown-villages').val();
	const payment = $('#dropdown-payment').val();

	console.log($('#phone').val())
	if (fullname.length == 0) {
		$(".check-name-field").css("display", "block");
		checkValidate = false;
	}
	if (phone.length != 10) {
		$(".check-phone-field").css("display", "block");
		checkValidate = false;
	}

	if (checkValidate == true) {

		var r = confirm("Bạn sẽ mua đơn hàng này? ");
		if (r == true) {
			$.ajax({

				type: "POST",
				url: "/OrderAPI/Order",
				data: {
					__RequestVerificationToken: token,
					fullname: fullname,
					phone: phone,
					village: village,
					address: address,
					payment: payment

				},
				success: function (value) {

						alert("Đặt hàng thành công!");
						location.reload();
					
				}, error: () => {
					console.log('Error');
				}

			})
		}

	}

})

$("#quantity").change(function () {
	var bookId = $(this).closest("tr").find("#idBook").val();
	var quantity = $(this).val();



	$.ajax({
		url: "/check-quantity",
		type: "POST",
		data: {
			id: bookId,
			quantity: quantity
		},
		success: function (value) {

			if (value != "-1") { //vượt quá số lượng hiện có
				alert("Vượt quá số lượng hiện có!\nSố lượng hiện tại là: " + value);
				location.reload();

			}
		}
	})
})











