using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.DTO
{
    public class BookDTO
    {
        public BookDTO()
        {
            priceDiscount = 0;
        }

        public long IdBook { get; set; }
        public string BookName { get; set; }
        public string DescribeBook { get; set; }
        public string Picture { get; set; }
        public float Price { get; set; }
        public DateTime PublishDay { get; set; }
        public int TotalQuantity { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public string CompanyName { get; set; }

        public string[] discountContent { get; set; }
        public float priceDiscount { get; set; }
    }
}
