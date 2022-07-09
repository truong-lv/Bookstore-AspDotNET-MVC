using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly ICategoryService categoryService;
        private readonly ICompanyService companyService;

        public BookController(ILogger<BookController> logger, BOOKSTOREContext context, IBookService bookService, IAuthorService authorService, ICategoryService categoryService, ICompanyService companyService)
        {
            _logger = logger;
            this.bookService = bookService;
            this.authorService = authorService;
            this.categoryService = categoryService;
            this.companyService = companyService;
        }

        public IActionResult Home(int currentPageIndex = 1)
        {
            BookPagineDTO books = bookService.GetBooks(currentPageIndex);
            ViewBag.listCategory = categoryService.getAllCategory();
            ViewBag.listTopBuy=bookService.getTopBuy().Concat(bookService.getTopBuy());
            return View("~/Views/Home/Home.cshtml",books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
