using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Models.ModelView;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService itemService;
        private readonly IOrderService orderService;
        public ItemController(ILogger<ItemController> logger, IItemService itemService,IOrderService orderService)
        {
            _logger = logger;
            this.itemService = itemService;
            this.orderService=orderService;
    }
        public IActionResult UserCart()
        {
            
            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            UserItem userItem= itemService.GetUserItems(userId);

            return View("~/Views/User/UserCart.cshtml", userItem);
        }

        public async Task<IActionResult> OrderAgain(long orderId)
        {

            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Order order = orderService.findOrderById(orderId);
            foreach(OrderDetail orderDetail in order.OrderDetails)
            {
                Item item = new Item();
                item.IdBook = orderDetail.IdBook;
                item.QuantityBooks = orderDetail.Quantity;
                item.UserId = userId;
                await itemService.addItem(item);
            }
            

            return RedirectToAction(nameof(UserCart));
        }
    }
}
