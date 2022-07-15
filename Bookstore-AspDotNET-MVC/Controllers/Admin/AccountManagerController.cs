using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Service;
using Bookstore_AspDotNET_MVC.utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Admin
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "ROLE_ADMIN")]
    public class AccountManagerController : Controller
    {
        private readonly ILogger<AccountManagerController> _logger;
        private readonly IAccountService accountService;

        public AccountManagerController(ILogger<AccountManagerController> logger, IAccountService accountService)
        {
            _logger = logger;
            this.accountService = accountService;
        }
        // GET: AccountManagerController
        public ActionResult Index(int currentPageIndex = 1)
        {
            @ViewData["Account"] = "active";
            
            return View("/Views/Admin/Account/Index.cshtml",accountService.GetAccounts(currentPageIndex));
        }

        public ActionResult AddOrEditUser(long id = 0)
        {
            if (id == 0)
            {
                return View("/Views/Admin/Account/AddOrEditUser.cshtml", new Userinfor());
            }
            else
            {
                var user = accountService.findUserById(id);
                HashPassword hashPassword = new HashPassword();
                user.Password = hashPassword.DecryptString(user.Password);
                if (user == null)
                {
                    return NotFound();
                }
                return View("/Views/Admin/Account/AddOrEditUser.cshtml", user);
            }


        }

        //POST: BookManagerController/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditUser(long id, Userinfor user)
        {
            HashPassword hashPassword = new HashPassword();
            if (ModelState.IsValid)
            {
                string checkErorr ="";
                ViewBag.Erorr = "";
                if (id == 0)
                {
                    try
                    {
                        checkErorr = accountService.checkUserExist(user);
                        if (checkErorr == "")
                        {
                            user.Password = hashPassword.EncryptString(user.Password);
                            await accountService.addUser(user);
                        }
                        else {
                            ViewBag.Erorr = checkErorr;
                            return View("/Views/Admin/Account/AddOrEditUser.cshtml", user);
                        }
                    }catch(Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                else
                {
                    try
                    {
                        checkErorr = accountService.checkUserUpdateExist(user);
                        if (checkErorr == "")
                        {
                            user.Password = hashPassword.EncryptString(user.Password);
                            await accountService.updateUser(user);
                        }
                        else {
                            ViewBag.Erorr = checkErorr;
                            return View("/Views/Admin/Account/AddOrEditUser.cshtml", user);
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (accountService.findUserById(id) == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Ok("Thêm user thành công!!");
            }
            else
            {
                return View("/Views/Admin/Account/AddOrEditUser.cshtml", user);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var user = accountService.findUserById(id);
            bool isDelete = await accountService.deleteUser(user);
            if (isDelete)
            {
                return Ok("Xóa tài khoản thành công!!");
            }
            else
            {
                return BadRequest("Xóa tài khoản thất bại!!");
            }
        }
    }
}
