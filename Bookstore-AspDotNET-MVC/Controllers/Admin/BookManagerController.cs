using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Admin
{
    public class BookManagerController : Controller
    {
        private readonly ILogger<BookManagerController> _logger;
        private readonly BOOKSTOREContext _context;
        private readonly BookService bookService;
        private readonly AuthorService authorService;
        private readonly CategoryService categoryService;
        private readonly CompanyService companyService;

        public BookManagerController(ILogger<BookManagerController> logger, BOOKSTOREContext context)
        {
            _logger = logger;
            _context = context;
            bookService = new BookService(context);
            authorService = new AuthorService(context);
            categoryService = new CategoryService(context);
            companyService = new CompanyService(context);
        }
        // GET: BookManagerController
        public ActionResult Index(int currentPageIndex=1)
        {
            ViewData["Title"] = "Book Manager";
            ViewData["Book"] = "active";
            return View("/Views/Admin/Book/BookManager.cshtml", bookService.GetBooks(currentPageIndex));
        }


        // GET: BookManagerController/Create
        public ActionResult AddOrEditBook(long id=0)
        {
            ViewBag.Category = categoryService.getAllCategory();
            ViewBag.Author = authorService.getAllAuthor();
            ViewBag.Company = companyService.getAllCompany();



            if (id == 0)
            {
                return View("/Views/Admin/Book/AddOrEditBook.cshtml", new Book());
            }
            else
            {
                var book = bookService.findBookById(id);
                if (book == null)
                {
                    return NotFound();
                }
                return View("/Views/Admin/Book/AddOrEditBook.cshtml", book);

            }


        }

        // POST: BookManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditBook(long id, Book book)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await bookService.addBook(book);
                }
                else
                {
                    try{
                        await bookService.updateBook(book);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (bookService.findBookById(id)==null)
                        {
                            return NotFound();
                        }else
                        {
                            throw;
                        }
                    }
                }

                return Ok("Thêm sách thành công!!");
            }
            else
            {
                ViewBag.Category = categoryService.getAllCategory();
                ViewBag.Author = authorService.getAllAuthor();
                ViewBag.Company = companyService.getAllCompany();

                return View("/Views/Admin/Book/AddOrEditBook.cshtml", book);
            }
            
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var book = bookService.findBookById(id);
            bool isDelete=await bookService.deleteBook(book);
            if (isDelete)
            {
                return Ok("Xóa sách thành công!!");
            }
            else
            {
                return BadRequest("Xóa sách thất bại!!");
            }
        }
    }
}
