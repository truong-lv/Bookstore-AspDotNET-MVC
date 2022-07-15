using Bookstore_AspDotNET_MVC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Models.ModelView
{
    public class ItemDTO
    {
        public ItemDTO()
        {

        }

        public Item item { get; set; }

        public BookDTO bookDTO { get; set; }

        public float realPrice() {
            return bookDTO.Price - bookDTO.Price * bookDTO.priceDiscount / 100;
        }
    }
}
