using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Admin
{
    public class ReviewManagerController : Controller
    {
        private readonly ILogger<ReviewManagerController> _logger;
        private readonly BOOKSTOREContext _context;
        private readonly ReviewService reviewService;
        private readonly BookService bookService;
        public ReviewManagerController(ILogger<ReviewManagerController> logger, BOOKSTOREContext context)
        {
            _logger = logger;
            _context = context;
            reviewService = new ReviewService(context);
            bookService = new BookService(context);
        }
        public IActionResult Index(int currentPageIndex = 1)
        {
            ViewData["Review"] = "active";
            return View("/Views/Admin/Review/Index.cshtml", reviewService.GetBookReview(currentPageIndex));
        }

        public IActionResult Detail(int id)
        {
            ViewData["Review"] = "active";
            Book book = bookService.findBookById(id);
            book.IdAuthorNavigation = _context.Authors.Find(book.IdAuthor);
            book.Category = _context.Categories.Find(book.CategoryId);
            book.IdCompanyNavigation = _context.PublishingCompanies.Find(book.IdCompany);
            book.Reviews = _context.Reviews.Where(r => r.IdBook == book.IdBook).OrderByDescending(r => r.Time).ToList();
            foreach(var review in book.Reviews)
            {
                review.IdBookNavigation = _context.Books.Find(review.IdBook);
                review.User = _context.Userinfors.Find(review.UserId);
            }

            return View("/Views/Admin/Review/ReviewDetail.cshtml", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long idBook, long idUser)
        {
            var review = reviewService.findReviewById(idBook,idUser);
            bool isDelete = await reviewService.deleteReview(review);
            if (isDelete)
            {
                return Ok("Xóa Bình luận thành công!!");
            }
            else
            {
                return BadRequest("Xóa bình luận thất bại!!");
            }
        }
    }
}
