using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class ReviewService:IReviewService
    {
        private readonly BOOKSTOREContext _context;

        public ReviewService(BOOKSTOREContext context)
        {
            _context = context;
        }

        public BookReviewPagineDTO GetBookReview(int currentPage)
        {
            int maxRows = 10;
            var books = _context.Books.ToList();

            List<Book> listBook=books.OrderBy(book => book.IdBook)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();






            BookReviewPagineDTO bookReviewPagineDTO = new BookReviewPagineDTO();

            List<BookReviewDTO> bookReviewDTOs = new List<BookReviewDTO>();

            foreach(var book in listBook)
            {
                BookReviewDTO bookReviewDTOtemp = new BookReviewDTO();
                bookReviewDTOtemp.Book = book;
                bookReviewDTOtemp.ListStart = getBookStartByIdBook(book.IdBook);
                bookReviewDTOs.Add(bookReviewDTOtemp);
            }

            bookReviewPagineDTO.BookReviewDTOs = bookReviewDTOs;


            double pageCount = (double)((decimal)books.Count() / Convert.ToDecimal(maxRows));

            bookReviewPagineDTO.PageCount = (int)Math.Ceiling(pageCount);

            bookReviewPagineDTO.CurrentPageIndex = currentPage;

            return bookReviewPagineDTO;
        }

        public List<StarDTO> getBookStartByIdBook(long id)
        {
           List<StarDTO> listStart = (from review in _context.Reviews
                                    where review.IdBook == id
                                    orderby review.Star descending
                                    group review by review.Star into grp
                                    select new StarDTO( grp.Key, grp.Count() )).ToList();

            return listStart;
        }

        public Review findReviewById(long idBook, long idUser)
        {
            return _context.Reviews.Find(idBook,idUser);
        }

        public async Task<bool> deleteReview(Review review)
        {
            try
            {
                _context.Remove(review);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> addReview(Review review)
        {
            try
            {
                _context.Add(review);
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
