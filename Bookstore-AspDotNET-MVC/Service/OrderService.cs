using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class OrderService:IOrderService
    {
        private readonly BOOKSTOREContext _context;

        public OrderService(BOOKSTOREContext context)
        {
            _context = context;
        }

        public OrderPagineDTO GetOrdersByStatus(int currentPage,int status=-1)
        {
            int maxRows = 10;
            List<Order> orders ;

            //nếu status =-1 thì lấy tất cả đơn hàng (default)
            if (status == -1) {
                orders = _context.Orders.ToList();
            }
            else//ngược lại thì lấy theo status đã yêu cầu
            {
                orders = _context.Orders.Where(order=>order.OrderStatus==status).ToList();
            }

            OrderPagineDTO orderPagine = new OrderPagineDTO();
            
            orderPagine.Orders = orders.OrderBy(order => order.OrderDay)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            double pageCount = (double)((decimal)orders.Count() / Convert.ToDecimal(maxRows));

            orderPagine.Status = status;

            orderPagine.PageCount = (int)Math.Ceiling(pageCount);

            orderPagine.CurrentPageIndex = currentPage;

            return orderPagine;
        }

        public OrderPagineDTO GetOrdersByTime(DateTime startTime, DateTime endTime, int currentPage, int status = -1)
        {
            int maxRows = 10;
            OrderPagineDTO orderPagine = GetOrdersByStatus(currentPage, status = -1);
            

            List<Order> list = orderPagine.Orders
                                    .Where(order => (order.OrderDay >= startTime && order.OrderDay <= endTime))
                                    .ToList();
            orderPagine.Orders = list;

            double pageCount = (double)((decimal)list.Count() / Convert.ToDecimal(maxRows));

            orderPagine.PageCount = (int)Math.Ceiling(pageCount);

            orderPagine.CurrentPageIndex = currentPage;

            return orderPagine;
        }

        public async Task<bool> OrderConfirm(long id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return false;

            order.OrderStatus = 1;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> OrderCancle(long id)
        {
            var order=_context.Orders.Find(id);
            if (order == null) return false;

            order.OrderStatus = 4;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public Order findOrderById(long id)
        {
            return _context.Orders.Find(id);
        }
    }
}
