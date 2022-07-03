// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';
const a = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"];

$.ajax({
	type: "GET",
	url: "https://localhost:44333/Statistict/GetMoneyPerMonthByYear",
	success: function (res) {
		const resulft= res
		// Area Chart Example
		var ctx = document.getElementById("myAreaChart");
		var myLineChart = new Chart(ctx, {
			type: 'line',
			data: {
				labels: a,
				datasets: [{
					label: "Lợi nhuận",
					lineTension: 0.3,
					backgroundColor: "rgba(2,117,216,0.2)",
					borderColor: "rgba(2,117,216,1)",
					pointRadius: 5,
					pointBackgroundColor: "rgba(2,117,216,1)",
					pointBorderColor: "rgba(255,255,255,0.8)",
					pointHoverRadius: 5,
					pointHoverBackgroundColor: "rgba(2,117,216,1)",
					pointHitRadius: 50,
					pointBorderWidth: 2,
					data: resulft,
				}],
			},
			options: {
				scales: {
					xAxes: [{
						time: {
							unit: 'date'
						},
						gridLines: {
							display: false
						},
						ticks: {
							maxTicksLimit: 12
						}
					}],
					yAxes: [{
						ticks: {
							min: 0,
							max: Math.max(...resulft),
							maxTicksLimit: 6
						},
						gridLines: {
							color: "rgba(0, 0, 0, .125)",
						}
					}],
				},
				legend: {
					display: false
				}
			}
		});
	},
	error: function (er) {
		console.log(er.responseText);
	}

})




