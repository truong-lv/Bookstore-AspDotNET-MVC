using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.IService
{
    public interface IOrderService
    {
        OrderPagineDTO GetOrdersByStatus(int currentPage, int status = -1);

        OrderPagineDTO GetUserOrder(int currentPage, long idUser);

        OrderPagineDTO GetOrdersByTime(DateTime startTime, DateTime endTime, int currentPage, int status = -1);

        Task<bool> OrderConfirm(long id);
        Task<bool> OrderInvited(long id);

        Task<bool> OrderCancle(long id);

        Order findOrderById(long id);

        List<float> getMoneyPerMonthByYear(int year);

        List<int> getListOrderYear();

        float getTotalProfit();

        int getTotalOder();

        float getAveragePrice();

        bool createNew(string name, string phone, long userId, UserItem list, string addressName, long wardId, long payment);

    }
}
