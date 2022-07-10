$("#btn-add-to-cart").click(function() {
			
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
		success: function (value) {
			$("#quantity").val('');
			alert(value); 
		},error: (er) => {
			console.log(er);
	}

	})
})













