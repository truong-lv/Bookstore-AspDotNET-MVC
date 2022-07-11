using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.IService
{
    public interface IBookService
    {
        BookPagineDTO GetBooks(int currentPage);

        BookDTOPagine GetBookDTOs(int currentPage);

        Book findBookById(long idBook);

        Book findBookReviewById(long idBook);
        Task<bool> addBook(Book book);

        Task<bool> updateBook(Book book);

        Task<bool> deleteBook(Book book);

        List<BookDTO> getTopBuy();

        List<BookDTO> getTopNew();

        List<BookDTO> getBookSameAuthor(long idAuthor);

        List<BookDTO> getBookSameCategory(long idCategory, int quantity=100);

        List<BookDTO> getBookKeySearch(string key);

        BookDTO conveBookDTO(long idBook);

    }
}
