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
using System.Security.Claims;
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
        private readonly IReviewService reviewService;

        public BookController(ILogger<BookController> logger, BOOKSTOREContext context, IBookService bookService, IAuthorService authorService, ICategoryService categoryService, ICompanyService companyService, IReviewService reviewService)
        {
            _logger = logger;
            this.bookService = bookService;
            this.authorService = authorService;
            this.categoryService = categoryService;
            this.companyService = companyService;
            this.reviewService = reviewService;
        }

        public IActionResult Home(int currentPageIndex = 1)
        {
            BookPagineDTO books = bookService.GetBooks(currentPageIndex);
            ViewBag.listCategory = categoryService.getAllCategory();
            ViewBag.listTopBuy=bookService.getTopBuy();
            ViewBag.listTopNew=bookService.getTopNew();
            return View("~/Views/Home/Home.cshtml",books);
        }

        public ActionResult ProductDetail(long id)
        {
            Book book = bookService.findBookReviewById(id);
            string[] listDecription = book.DescribeBook.Split('\n');
            List<Book> listBookSameAuthor = bookService.getBookSameAuthor(book.IdAuthor);
            listBookSameAuthor.Remove(book);


            bool checkReview = false;
            if (User.Identity.IsAuthenticated)
            {
                long userId =long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (reviewService.findReviewById(id, userId) != null) { checkReview = true; }
            }

            ViewBag.listDecription = listDecription;
            ViewBag.listBookSameAuthor = listBookSameAuthor;

            ViewBag.checkReview = checkReview;
            ViewData["Title"] = book.BookName;

            return View("~/Views/Product/Detail.cshtml", book);
        }

        public IActionResult GetBookSameCategory(long idCategory)
        {
            Category category = categoryService.getCategoryById(idCategory);
            List<Book> list = bookService.getBookSameCategory(idCategory);
            ViewData["Title"] = category.Name.ToUpper();

            return View("~/Views/Product/Search.cshtml", list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
