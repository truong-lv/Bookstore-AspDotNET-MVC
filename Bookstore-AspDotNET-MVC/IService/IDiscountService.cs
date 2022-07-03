using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.IService
{
    public interface IDiscountService
    {
        DiscountPagineDTO GetDiscounts(int currentPage, int status = -1);

        Discount findDiscountById(long id);

        Task<bool> addDiscount(Discount discount);

        Task<bool> updateDiscount(Discount discount);

        Task<bool> deleteDiscount(Discount discount);
    }
}
