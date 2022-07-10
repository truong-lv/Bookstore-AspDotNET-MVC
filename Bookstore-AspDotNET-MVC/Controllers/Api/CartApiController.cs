using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Api
{
    public class CartApiController : Controller
    {
        private readonly ILogger<CartApiController> _logger;
        private readonly IItemService itemService;

        public CartApiController(ILogger<CartApiController> logger, IItemService itemService)
        {
            _logger = logger;
            this.itemService = itemService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate(long bookId, int quantity)
        {
            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool isUpdate = false;
            

            var item = itemService.findItemById(bookId, userId);

            if (item != null)
            {
                item.QuantityBooks += quantity;
                isUpdate = await itemService.updateItem(item);
            }
            else
            {
                item = new Item();
                item.IdBook = bookId;
                item.UserId = userId;
                item.QuantityBooks = quantity;
                isUpdate = await itemService.addItem(item);
            }

            if (isUpdate)
            {
                return Ok("Cập nhập giỏ hàng thành công!!");
            }
            else
            {
                return BadRequest("Cập nhập giỏ hàng thất bại!!");
            }
        }
    }
}
