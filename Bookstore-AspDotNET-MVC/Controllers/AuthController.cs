using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        //public ActionResult Login()
        //{
        //    return View("Views/Login/LoginPage.cshtml");
        //}

        public IActionResult Login()
        {

            LoginModel objLoginModel = new LoginModel();
            return View("Views/Login/LoginPage.cshtml",objLoginModel);
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                Userinfor user = _context.Userinfors.Include(u=>u.UserRoles)
                                                .ThenInclude(ur=>ur.IdRoleNavigation)
                                                .Where(u => u.Username == objLoginModel.UserName && u.Password == objLoginModel.Password)
                                                .FirstOrDefault();
                
                if (user != null)
                {

                    //A claim is a statement about a subject by an issuer and    
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        //new Claim(ClaimTypes.Role, user.UserRoles.ElementAt(0).IdRoleNavigation.RoleName),
                    };



                    foreach (var role in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.IdRoleNavigation.RoleName));
                    }


                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = objLoginModel.RememberLogin
                    });



                    return RedirectToAction("Home", "Book");
                    //return LocalRedirect("~/BookManager/Index");
                }
                else
                {
                    ViewBag.Message = "Tài khoản và mật khẩu không hợp lệ!!";
                    return View("Views/Login/LoginPage.cshtml", objLoginModel);
                }
            }
            return View("Views/Login/LoginPage.cshtml", objLoginModel);
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return RedirectToAction(nameof(Login));
        }


    }
}
