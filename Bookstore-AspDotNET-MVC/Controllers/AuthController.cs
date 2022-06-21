using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly BOOKSTOREContext _context;

        public AuthController(ILogger<AuthController> logger, BOOKSTOREContext context)
        {
            _logger = logger;
            _context = context;
        }
        // get login view
        public ActionResult Login()
        {
            return View("Views/Login/LoginPage.cshtml");
        }


        // POST: login with username vs password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String username, String pass)
        {

            var user = _context.Userinfors.Where(u => u.Username == username && u.Password == pass).FirstOrDefault();
            if (user != null)
            {
                int checkAdmin = user.UserRoles.Count(ur => ur.IdRole == 1);
                HttpContext.Session.SetString("username", user.Username);
                if (checkAdmin == 0)
                {
                    HttpContext.Session.SetString("role", "ROLE_USER");
                }
                else
                {
                    HttpContext.Session.SetString("role", "ROLE_ADMIN");
                }
                return Redirect(Url.Action("Index", "BookManager"));
            }
            else
            {
                TempData["ThongBao"] = "Tài khoản hoặc mật khẩu không hợp lệ";
                return RedirectToAction(nameof(Login));
            }

        }
        // Logout
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Login));
        }

       
    }
}
