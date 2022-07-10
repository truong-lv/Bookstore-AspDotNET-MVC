$(".rating-submit").click(function () {
	var form = $('#__AjaxAntiForgeryForm');
	var token = $('input[name="__RequestVerificationToken"]', form).val();

	var rating = parseFloat($('input[name="rating"]:checked').val());
	var comment = $("#comment").val(); 
	var bookId = $("#idBook").val();
		
	 	
	$.ajax({
		
		type: "POST",
        url: "/ReviewApi/AddOrUpdate",
		data: {
			__RequestVerificationToken: token,
			idBook: bookId, 
			star : rating, 
			comment: comment
		},
		success: function(value) {
			
			location.reload(); 
		},error: () => {
		console.log('Error');
	}

	})
})

$("#btn-review-again").click(function() {
		$("#inform-review-message").hide(); 
		$("#review-again-block").show(); 
})

































