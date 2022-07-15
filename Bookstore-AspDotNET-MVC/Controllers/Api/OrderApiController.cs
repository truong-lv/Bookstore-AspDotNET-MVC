using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Api
{
    public class OrderApiController : Controller
    {
        private readonly ILogger<OrderApiController> _logger;
        private readonly IOrderService orderService;
        private readonly IItemService itemService;

        public OrderApiController(ILogger<OrderApiController> logger, IOrderService orderService, IItemService itemService)
        {
            _logger = logger;
            this.orderService = orderService;
            this.itemService = itemService;

        }
        public IActionResult OrderDetail(long orderId)
        {
            Order order = orderService.findOrderById(orderId);
            return View("~/Views/User/OrderDetailPractical.cshtml", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(string fullname, string phone, long village, string address,long payment)
        {
            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            UserItem userItem = itemService.GetUserItems(userId);
            bool isOrder = orderService.createNew(fullname, phone, userId, userItem, address, village, payment);
            if (isOrder)
            {
                await itemService.deleteUserItem(userId);
                return Ok("Đặt hàng thành công");
            }
            return BadRequest("Đặt hàng thất bại");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderInvited(long orderId)
        {

            bool isCancle = await orderService.OrderInvited(orderId);
            if (isCancle)
            {
                return Ok("Xác nhận đơn hàng thành công");
            }
            else
            {
                return BadRequest("Xác nhận đơn hàng thất bại");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancleOrder(long orderId)
        {

            bool isCancle = await orderService.OrderCancle(orderId);
            if (isCancle)
            {
                return Ok("Hủy đơn hàng thành công");
            }
            else
            {
                return BadRequest("Hủy đơn hàng thất bại");
            }
        }
    }
}
