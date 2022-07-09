using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Erorr
{
    public class ErorrController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
