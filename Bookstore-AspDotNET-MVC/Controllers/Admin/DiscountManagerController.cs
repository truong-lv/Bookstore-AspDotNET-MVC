using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.IService;

namespace Bookstore_AspDotNET_MVC.Controllers.Admin
{
    public class DiscountManagerController : Controller
    {
        private readonly IDiscountService discountService;

        public DiscountManagerController(IDiscountService discountService)
        {
            this.discountService = discountService;
        }

        // GET: DiscountManager
        public IActionResult Index(int currentPageIndex = 1, int status = -1)
        {
            ViewData["Discount"] = "active";
            return View("/Views/Admin/Discount/Index.cshtml", discountService.GetDiscounts(currentPageIndex,status));
        }

        public ActionResult AddOrEditDiscount(long id = 0)
        {
            if (id == 0)
            {
                return View("/Views/Admin/Discount/AddOrEditDiscount.cshtml", new Discount());
            }
            else
            {
                var discount = discountService.findDiscountById(id);
                if (discount == null)
                {
                    return NotFound();
                }
                return View("/Views/Admin/Discount/AddOrEditDiscount.cshtml", discount);

            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditDiscount(long id, Discount discount)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await discountService.addDiscount(discount);
                }
                else
                {
                    try
                    {
                        await discountService.updateDiscount(discount);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (discountService.findDiscountById(id) == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Ok("Thêm thành công!!");
            }
            else
            {

                return View("/Views/Admin/Discount/AddOrEditDiscount.cshtml", discount);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var discount = discountService.findDiscountById(id);
            bool isDelete = await discountService.deleteDiscount(discount);
            if (isDelete)
            {
                return Ok("Xóa khuyến mãi thành công!!");
            }
            else
            {
                return BadRequest("Xóa khuyến mãi thất bại!!");
            }
        }
    }
}
