using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Admin
{
    public class OrderManagerController : Controller
    {
        private readonly ILogger<BookManagerController> _logger;
        private readonly BOOKSTOREContext _context;
        private readonly OrderService orderService;
        public OrderManagerController(ILogger<BookManagerController> logger, BOOKSTOREContext context)
        {
            _logger = logger;
            _context = context;
            orderService = new OrderService(context);
        }
        // GET: OrderManagerController
        public ActionResult Index(int currentPageIndex = 1, int status=-1)
        {
            ViewData["Order"] = "active";

            return View("/Views/Admin/Order/Index.cshtml",orderService.GetOrdersByStatus(currentPageIndex,status));
        }

        // GET: OrderManagerController/Details/5
        public ActionResult Detail(long id)
        {
            Order order = orderService.findOrderById(id);
            return Ok(order.OrderDetails.Count.ToString());

            if (order==null)
            {
                return NotFound("Sách không tồn tại");
            }
            else
            {
                return View("/Views/Admin/Order/OrderDetail.cshtml", order);

            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderConfirm(long id)
        {
            await orderService.OrderConfirm(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: OrderManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderCancle(long id)
        {
            await orderService.OrderCancle(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
