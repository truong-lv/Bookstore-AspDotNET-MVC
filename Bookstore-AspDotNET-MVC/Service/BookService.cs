using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class BookService:IBookService
    {
        private readonly BOOKSTOREContext _context;

        public BookService(BOOKSTOREContext context)
        {
            _context = context;
        }

        public BookPagineDTO GetBooks(int currentPage)
        {
            int maxRows = 10;
            var books = _context.Books.Include(b=>b.IdAuthorNavigation).ToList();

            BookPagineDTO bookPagine = new BookPagineDTO();

            bookPagine.Books = books.OrderBy(book => book.IdBook)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            double pageCount = (double)((decimal)books.Count() / Convert.ToDecimal(maxRows));

            bookPagine.PageCount = (int)Math.Ceiling(pageCount);

            bookPagine.CurrentPageIndex = currentPage;

            return bookPagine;
        }

        public BookDTOPagine GetBookDTOs(int currentPage)
        {
            int maxRows = 10;
            List<BookDTO> books = _context.Books
                                .Include(b => b.IdAuthorNavigation)
                                .Include(b => b.Category)
                                .Include(b => b.IdCompanyNavigation)
                                .Include(b => b.BookDiscounts)
                                .ThenInclude(bd => bd.IdDiscountNavigation)
                                .Select(b=>converBookToBookDTO(b)).ToList();

            BookDTOPagine bookPagine = new BookDTOPagine();

            bookPagine.Books = books.OrderBy(book => book.IdBook)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            double pageCount = (double)((decimal)books.Count() / Convert.ToDecimal(maxRows));

            bookPagine.PageCount = (int)Math.Ceiling(pageCount);

            bookPagine.CurrentPageIndex = currentPage;

            return bookPagine;
        }

        public Book findBookById(long idBook)
        {
            return _context.Books.Find(idBook);
        }

        public Book findBookReviewById(long idBook)
        {

            return _context.Books.Include(b => b.Category)
                                  .Include(b => b.IdAuthorNavigation)
                                  .Include(b => b.IdCompanyNavigation)
                                  .Include(b => b.BookDiscounts)
                                  .ThenInclude(bd => bd.IdDiscountNavigation)
                                  .Include(b => b.Reviews)
                                  .ThenInclude(r => r.User)
                                  .First(b=>b.IdBook==idBook);
        }

        public async Task<bool> addBook(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updateBook(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteBook(Book book)
        {
            try
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
                return true;    

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<BookDTO> getTopBuy()
        {

            List<BookDTO> list =  _context.OrderDetails.Where(o=>o.Order.OrderStatus==3)
                               .Include(od=>od.IdBookNavigation)
                               .ThenInclude(b => b.IdAuthorNavigation)
                               .Include(od => od.IdBookNavigation)
                               .ThenInclude(b => b.Category)
                               .Include(od => od.IdBookNavigation)
                               .ThenInclude(b => b.IdCompanyNavigation)
                               .Include(od => od.IdBookNavigation)
                               .ThenInclude(b => b.BookDiscounts)
                               .ThenInclude(bd => bd.IdDiscountNavigation)
                               .ToList()
                               .GroupBy(ob => ob.IdBookNavigation)
                               .OrderByDescending(b => b.Sum(x => x.IdBook))
                               .Take(10)
                               .Select(b => converBookToBookDTO(b.Key))
                               .ToList();
            return list;
        }

        public List<BookDTO> getTopNew()
        {
            List<BookDTO> list = _context.Books
                               .Include(b => b.IdAuthorNavigation)
                               .Include(b => b.Category)
                               .Include(b => b.IdCompanyNavigation)
                               .Include(b=>b.BookDiscounts)
                               .ThenInclude(bd=>bd.IdDiscountNavigation).ToList()
                               .OrderByDescending(b => b.PublishDay).Select(b => converBookToBookDTO(b))
                               .Take(10)
                               .ToList();
            return list;
        }

        public List<BookDTO> getBookSameAuthor(long idAuthor)
        {
            List<BookDTO> list = _context.Books.Where(b=>b.IdAuthor==idAuthor)
                                .Include(b => b.IdAuthorNavigation)
                               .Include(b => b.Category)
                               .Include(b => b.IdCompanyNavigation)
                               .Include(b => b.BookDiscounts)
                               .ThenInclude(bd => bd.IdDiscountNavigation).ToList()
                               .OrderByDescending(b => b.PublishDay).Select(b => converBookToBookDTO(b))
                               .Take(11)
                               .ToList();
            return list;
        }
        
        public List<BookDTO> getBookSameCategory(long idCategory, int quantity=100)
        {
            List<BookDTO> list = _context.Books.Where(b=>b.CategoryId==idCategory)
                                .Include(b => b.IdAuthorNavigation)
                               .Include(b => b.Category)
                               .Include(b => b.IdCompanyNavigation)
                               .Include(b => b.BookDiscounts)
                               .ThenInclude(bd => bd.IdDiscountNavigation).ToList()
                               .OrderByDescending(b => b.PublishDay).Select(b => converBookToBookDTO(b))
                               .Take(quantity)
                               .ToList();
            return list;
        }

        public List<BookDTO> getBookKeySearch(string key)
        {
            List<BookDTO> books = _context.Books
                                .Where(b => b.BookName.Contains(key) 
                                        || b.Category.Name.Contains(key)
                                        || b.IdAuthorNavigation.AuthorName.Contains(key)
                                        || b.IdCompanyNavigation.CompanyName.Contains(key))
                                .Include(b => b.IdAuthorNavigation)
                               .Include(b => b.Category)
                               .Include(b => b.IdCompanyNavigation)
                               .Include(b => b.BookDiscounts)
                               .ThenInclude(bd => bd.IdDiscountNavigation).ToList()
                               .OrderByDescending(b => b.PublishDay).Select(b => converBookToBookDTO(b))
                               .Take(10)
                               .ToList();

            return books;
        }

        public BookDTO conveBookDTO(long  idBook)
        {
            return _context.Books.Where(b=>b.IdBook==idBook)
                                .Include(b => b.IdAuthorNavigation)
                               .Include(b => b.Category)
                               .Include(b => b.IdCompanyNavigation)
                               .Include(b => b.BookDiscounts)
                               .ThenInclude(bd => bd.IdDiscountNavigation)
                               .Select(b=>converBookToBookDTO(b))
                               .FirstOrDefault();
        }

       public static BookDTO converBookToBookDTO(Book b)
        {
            BookDTO bookDTO = new BookDTO()
            {
                IdBook = b.IdBook,
                BookName = b.BookName,
                DescribeBook = b.DescribeBook,
                Picture = b.Picture,
                Price = b.Price,
                PublishDay = b.PublishDay,
                TotalQuantity = b.TotalQuantity,
                AuthorName = b.IdAuthorNavigation.AuthorName,
                CategoryName = b.Category.Name,
                CompanyName = b.IdCompanyNavigation.CompanyName,
                discountContent = b.BookDiscounts
                                                .Where(bd => bd.IdDiscountNavigation.StartTime < DateTime.Now && bd.IdDiscountNavigation.EndTime > DateTime.Now)
                                                .Select(bd => bd.IdDiscountNavigation.ContentDiscount).ToArray(),
                priceDiscount = b.BookDiscounts
                                            .Where(bd => bd.IdDiscountNavigation.StartTime < DateTime.Now && bd.IdDiscountNavigation.EndTime > DateTime.Now)
                                            .Sum(bd => bd.IdDiscountNavigation.DiscountPercent)
            };

            return bookDTO;
        }
    }
}
