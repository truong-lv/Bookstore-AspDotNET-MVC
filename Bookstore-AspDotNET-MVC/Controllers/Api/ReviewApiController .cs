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
    public class ReviewApiController : Controller
    {
        private readonly ILogger<ReviewApiController> _logger;
        private readonly IReviewService reviewService;

        public ReviewApiController(ILogger<ReviewApiController> logger, IReviewService reviewService)
        {
            _logger = logger;
            this.reviewService = reviewService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate(long idBook, int star, string comment)
        {
            long userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool isUpdate = false;
            

            var review = reviewService.findReviewById(idBook, userId);

            if (review != null)
            {
                review.Star = star;
                review.Comments = comment;
                review.Time = DateTime.Now;
                isUpdate = await reviewService.updateReview(review);
            }
            else
            {
                review = new Review();
                review.IdBook = idBook;
                review.UserId = userId;
                review.Star = star;
                review.Comments = comment;
                review.Time = DateTime.Now;
                isUpdate = await reviewService.addReview(review);
            }

            if (isUpdate)
            {
                return Ok("Bình luận thành công!!");
            }
            else
            {
                return BadRequest("Bình luận thất bại!!");
            }
        }
    }
}
