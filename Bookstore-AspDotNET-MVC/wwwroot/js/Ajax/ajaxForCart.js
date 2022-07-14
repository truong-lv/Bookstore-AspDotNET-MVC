$("#btn-add-to-cart").click(function() {
	var form = $('#__AjaxAntiForgeryForm');
	var token = $('input[name="__RequestVerificationToken"]', form).val();

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
	var block = $(this).closest("div");
	var bookId = $(this).closest("div").find(".book").attr("data-idBook");
	var quantity = $(this).val();



	$.ajax({
		url: "/update-cart",
		type: "POST",
		data: {
			id: bookId,
			quantity: quantity
		},
		success: function (value) {

			if (value == "1") { //vượt quá số lượng hiện có
				alert("Vượt quá số lượng hiện có!");
				window.location.replace("/cart/");

			} else {

				var price = block.find(".book-price").attr("data-book-price");
				var priceOfItem = price * quantity;
				var priceOfItemFormat = priceOfItem.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace(".00", "");
				var totalPriceFormat = parseInt(value).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace(".00", "");
				block.find(".item-price").replaceWith('<p class="item-price">Thành tiền <span style="font-weight:bold">' + priceOfItemFormat + ' VND </span> </p>');
				$("#total-price").replaceWith('<p style="font-size:25px" id= "total-price">Tổng tiền <span style="font-weight:bold; color:red">' + totalPriceFormat + ' VND </span> </p>');
			}
		}
	})
})




$(".btn-delete-item").click(function () {

	var block = $(this).closest("div");
	var bookId = $(this).closest("div").find(".book").attr("data-idBook");

	$.ajax({

		type: "POST",
		url: "http://localhost:8080/remove-item",
		data: {
			id: bookId

		},
		success: function (value) {

			if (value == "false") {
				alert("Xóa thất bại");

			} else {
				if (value == 0) $("#btn-buy").hide();

				block.remove();
				var totalPriceFormat = parseInt(value).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace(".00", "");
				$("#total-price").replaceWith('<p style="font-size:25px" id= "total-price">Tổng tiền <span style="font-weight:bold; color:red">' + totalPriceFormat + ' VND </span> </p>');
			}
		}, error: () => {
			console.log('Error');
		}

	})
})
$("#btn-buy").click(function () {



	$.ajax({

		type: "POST",
		url: "http://localhost:8080/check-can-buy",
		data: {

		},
		success: function (value) {

			if (value == true) $("#block-info-buy").show();

		}, error: () => {
			console.log('Error');
		}

	})


})


$("#dropdown-province").change(function () {


	var provinceId = $(this).val();





	$.ajax({

		type: "POST",
		url: "http://localhost:8080/get-district",
		data: {
			provinceId: provinceId

		},
		success: function (value) {
			console.log(value);
			$("#dropdown-district").html(value);

		}, error: () => {
			console.log('Error');
		}

	})

})


$("#dropdown-district").change(function () {


	var districtId = $(this).val();





	$.ajax({

		type: "POST",
		url: "http://localhost:8080/get-village",
		data: {
			districtId: districtId

		},
		success: function (value) {

			$("#dropdown-village").html(value);

		}, error: () => {
			console.log('Error');
		}

	})

})


$("#btn-verify-buy").click(function () {
	$("#check-name-field").hide();
	$("#check-phone-field").hide();
	$(".alert-over-quantity").hide();

	var checkValidate = true;
	var fullname = $("#fullname").val();
	var phone = $("#phone").val();
	var address = $("#address").val();
	var village = $("#dropdown-village").val();


	if (fullname.length == 0) {
		$("#check-name-field").show();
		checkValidate = false;
	}
	if (phone.length != 10) {
		$("#check-phone-field").show();
		checkValidate = false;
	}

	if (checkValidate == true) {

		var r = confirm("Bạn sẽ mua đơn hàng này? ");
		if (r == true) {
			$.ajax({

				type: "POST",
				url: "http://localhost:8080/order",
				data: {
					fullname: fullname,
					phone: phone,
					village: village,
					address: address

				},
				success: function (value) {

					if (value == "false") {
						alert("Order thất bại");

					} else if (value == "true") {
						alert("Đặt hàng thành công!");
						location.reload();
					} else {
						let arr = value.split("-");
						var id = "#over-maximum-quantity-" + arr[0];
						$(id).find("p").replaceWith("<p>Số lượng còn lại chỉ là: " + arr[1] + "</p>");
						$(id).show();

					}
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











