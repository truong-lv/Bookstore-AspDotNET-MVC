using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAccountService accountService;
        private readonly IOrderService orderService;

        public UserController(ILogger<UserController> logger, IAccountService accountService, IOrderService orderService)
        {
            _logger = logger;
            this.accountService = accountService;
            this.orderService = orderService;
        }
        public IActionResult UserInfo()
        {

            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Userinfor userinfor = accountService.findUserById(userId);

            return View("~/Views/User/Info.cshtml", userinfor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(Userinfor user)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await accountService.updateUser(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (accountService.findUserById(user.UserId) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("~/Views/User/Info.cshtml", user);
            }
            else
            {
                return View("~/Views/User/Info.cshtml", user);
            }


        }

        public IActionResult UserOrder(int currentPageIndex = 1)
        {

            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            OrderPagineDTO orderPagineDTO = orderService.GetUserOrder(currentPageIndex, userId);

            return View("~/Views/User/OrderHistory.cshtml", orderPagineDTO);
        }
    }
}
