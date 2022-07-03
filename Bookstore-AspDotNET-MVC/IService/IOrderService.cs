using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.IService
{
    public interface IOrderService
    {
        OrderPagineDTO GetOrdersByStatus(int currentPage, int status = -1);

        OrderPagineDTO GetOrdersByTime(DateTime startTime, DateTime endTime, int currentPage, int status = -1);

        Task<bool> OrderConfirm(long id);

        Task<bool> OrderCancle(long id);

        Order findOrderById(long id);

        List<float> getMoneyPerMonthByYear(int year);

        List<int> getListOrderYear();

        float getTotalProfit();

        int getTotalOder();

        float getAveragePrice();

    }
}
