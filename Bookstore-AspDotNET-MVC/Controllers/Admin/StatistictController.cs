using Bookstore_AspDotNET_MVC.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Admin
{
    public class StatistictController : Controller
    {
        private readonly ILogger<StatistictController> _logger;
        private readonly IOrderService orderService;
        public StatistictController(ILogger<StatistictController> logger, IOrderService orderService)
        {
            this._logger = logger;
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            @ViewData["Statistict"] = "active";
            @ViewData["TotalProfit"] = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", orderService.getTotalProfit());
            @ViewData["TotalOder"] = orderService.getTotalOder();
            @ViewData["AveragePrice"] =String.Format(CultureInfo.InvariantCulture, "{0:0,0}", orderService.getAveragePrice());
            List<int> listYear = orderService.getListOrderYear();

            return View("/Views/Admin/Statisticts/Statisticts.cshtml", listYear);
        }

        public IActionResult GetMoneyPerMonthByYear(int year=2021)
        {
            List<float> data= orderService.getMoneyPerMonthByYear(year);

            return Ok(data);
        }
    }
}
