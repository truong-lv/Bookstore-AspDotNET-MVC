using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService itemService;

        public ItemController(ILogger<ItemController> logger, IItemService itemService)
        {
            _logger = logger;
            this.itemService = itemService;
        }
        public IActionResult UserCart()
        {
            
            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<Item> listItem = itemService.GetUserItems(userId);

            return View("~/Views/User/UserCart.cshtml", listItem);
        }
    }
}
