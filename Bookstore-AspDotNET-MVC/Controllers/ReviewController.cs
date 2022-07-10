using Bookstore_AspDotNET_MVC.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IReviewService reviewService;

        public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService)
        {
            _logger = logger;
            this.reviewService = reviewService;
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "ROLE_USER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(long idBook, long idUser)
        {
            var review = reviewService.findReviewById(idBook, idUser);
            bool isDelete = await reviewService.deleteReview(review);
            if (isDelete)
            {
                return Ok("Thêm Bình luận thành công!!");
            }
            else
            {
                return BadRequest("Thêm bình luận thất bại!!");
            }
        }
    }
}
