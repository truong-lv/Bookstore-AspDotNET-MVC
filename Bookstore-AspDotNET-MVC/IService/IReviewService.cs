using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.IService
{
    public interface IReviewService
    {
        BookReviewPagineDTO GetBookReview(int currentPage);

        List<StarDTO> getBookStartByIdBook(long id);

        Review findReviewById(long idBook, long idUser);

        Task<bool> deleteReview(Review review);

        Task<bool> addReview(Review review);

    }
}
