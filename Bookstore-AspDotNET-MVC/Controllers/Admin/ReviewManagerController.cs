using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.IService;
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
        private readonly IReviewService reviewService;
        private readonly IBookService bookService;
        public ReviewManagerController(ILogger<ReviewManagerController> logger, IReviewService reviewService, IBookService bookService)
        {
            _logger = logger;
            this.reviewService = reviewService;                                                                                                          
            this.bookService = bookService;
        }
        public IActionResult Index(int currentPageIndex = 1)
        {
            ViewData["Review"] = "active";
            return View("/Views/Admin/Review/Index.cshtml", reviewService.GetBookReview(currentPageIndex));
        }

        public IActionResult Detail(int id)
        {
            ViewData["Review"] = "active";
            Book book = bookService.findBookReviewById(id);

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
