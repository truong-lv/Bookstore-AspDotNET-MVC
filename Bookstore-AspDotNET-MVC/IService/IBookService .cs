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

        Book findBookById(long idBook);

        Task<bool> addBook(Book book);


        Task<bool> updateBook(Book book);

        Task<bool> deleteBook(Book book);

    }
}
