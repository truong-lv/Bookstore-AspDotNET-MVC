using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Models.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class OrderService:IOrderService
    {
        private readonly BOOKSTOREContext _context;
        private readonly IBookService bookService;
        private readonly IAddressService addressService;

        public OrderService(BOOKSTOREContext context, IBookService bookService, IAddressService addressService)
        {
            _context = context;
            this.bookService = bookService;
            this.addressService = addressService;
        }

        public OrderPagineDTO GetOrdersByStatus(int currentPage,int status=-1)
        {
            int maxRows = 10;
            List<Order> orders ;

            //nếu status =-1 thì lấy tất cả đơn hàng (default)
            if (status == -1) {
                orders = _context.Orders.OrderByDescending(order => order.OrderDay).ToList();
            }
            else//ngược lại thì lấy theo status đã yêu cầu
            {
                orders = _context.Orders.Where(order=>order.OrderStatus==status)
                                        .OrderByDescending(order => order.OrderDay)
                                         .ToList();
            }

            OrderPagineDTO orderPagine = new OrderPagineDTO();
            
            orderPagine.Orders = orders
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            double pageCount = (double)((decimal)orders.Count() / Convert.ToDecimal(maxRows));

            orderPagine.Status = status;

            orderPagine.PageCount = (int)Math.Ceiling(pageCount);

            orderPagine.CurrentPageIndex = currentPage;

            return orderPagine;
        }


        public OrderPagineDTO GetUserOrder(int currentPage, long idUser)
        {
            int maxRows = 3;
            List<Order> orders;

            orders = _context.Orders.Where(o=>o.UserId==idUser).OrderByDescending(o=>o.OrderDay)
                                    .Include(o=>o.Address)
                                        .ThenInclude(a=>a.Warrd)
                                            .ThenInclude(w=>w.District)
                                                .ThenInclude(d=>d.Province)
                                    .ToList();

            OrderPagineDTO orderPagine = new OrderPagineDTO();

            orderPagine.Orders = orders
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            double pageCount = (double)((decimal)orders.Count() / Convert.ToDecimal(maxRows));

            orderPagine.Status = -1;

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

            order.OrderStatus = 2;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> OrderInvited(long id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return false;

            order.OrderStatus = 3;
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
            var order = _context.Orders.Include(o => o.OrderDetails)
                                        .ThenInclude(od => od.IdBookNavigation)
                                            .ThenInclude(b=>b.IdCompanyNavigation)
                                        .Include(o => o.OrderDetails)
                                        .ThenInclude(od => od.IdBookNavigation)
                                            .ThenInclude(b => b.IdAuthorNavigation)
                                        .Include(o => o.OrderDetails)
                                        .ThenInclude(od => od.IdBookNavigation)
                                            .ThenInclude(b => b.Category)
                                        .First(o=>o.OrderId==id);
            
            return order;
        }

        public List<float> getMoneyPerMonthByYear(int year)
        {
            //Get price in year per month
            List<float> listPrice = _context.Orders.Where(o => o.OrderDay.Value.Year == year && o.OrderStatus == 3)
                                                    .GroupBy(o => o.OrderDay.Value.Month)
                                                    .Select(g => g.Sum(b => b.TotalPrice)).ToList();
            //Get month with above price 
            List<int> listMonth = _context.Orders.Where(o => o.OrderDay.Value.Year == year && o.OrderStatus == 3)
                                                    .GroupBy(o => o.OrderDay.Value.Month)
                                                    .Select(g=>g.Key).ToList();
            // Mapping price and month respectively
            //initial list
            List<float> listPricePerMonth = new List<float>();
            for(int i = 0; i <= 11; i++)
            {
                listPricePerMonth.Add(0);
            }

            for(int i=0;i<listPrice.Count;i++)
            {
                listPricePerMonth[listMonth[i]] = listPrice[i];
            }


            return listPricePerMonth;
        }

        public List<int> getListOrderYear()
        {
            return _context.Orders.Where(o => o.OrderStatus == 3)
                                    .GroupBy(o => o.OrderDay.Value.Year)
                                    .Select(g => g.Key).ToList();
        }

        public float getTotalProfit()
        {
            return _context.Orders.Sum(o => o.TotalPrice);
        }

        public int getTotalOder()
        {
            return _context.Orders.Count();
        }

        public float getAveragePrice()
        {
            int sumYear = this.getListOrderYear().Count();
            float totalPrice = this.getTotalProfit();

            return totalPrice / sumYear;
        }

        public bool createNew(string name, string phone, long userId, UserItem list, string addressName, long wardId, long payment)
        {
            try
            {
                ICollection<OrderDetail> orderDetails = new HashSet<OrderDetail>();
                Order order = new Order();
                foreach (ItemDTO item in list.itemDTOs)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.IdBook = item.item.IdBook;
                    detail.Price = item.realPrice();
                    Book book = bookService.findBookById(item.item.IdBook);
                    if(item.item.QuantityBooks> book.TotalQuantity)
                    {
                        return false;
                    }
                    detail.Quantity = item.item.QuantityBooks;
                    book.TotalQuantity -= detail.Quantity;
                    bookService.updateBook(book);

                    orderDetails.Add(detail);
                }

                long addressId= addressService.createAddressDetail(addressName,wardId);

                order.NameOfCustomer = name;
                order.PhoneOfCustomer = phone;
                order.OrderDetails = orderDetails;
                order.AddressId = addressId;
                order.OrderDay=DateTime.Now;
                order.OrderStatus = 1;
                order.TotalPrice = list.totalPrice();
                order.IdPayment = payment;
                order.UserId = userId;

                _context.Add(order);
                _context.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }



            return false;
        }
    }
}
