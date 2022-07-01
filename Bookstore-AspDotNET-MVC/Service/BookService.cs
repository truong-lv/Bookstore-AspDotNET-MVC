using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Http;
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
            var books = _context.Books.ToList();

            BookPagineDTO bookPagine = new BookPagineDTO();

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
    }
}
