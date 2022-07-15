using Bookstore_AspDotNET_MVC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Models.ModelView
{
    public class UserItem
    {
        public UserItem()
        {
            this.itemDTOs = new List<ItemDTO>();
        }

        public List<Ward> wards{ get; set; }
        public List<District> districts{ get; set; }
        public List<Province> provinces{ get; set; }
        public List<Payment> payments{ get; set; }
        public List<ItemDTO> itemDTOs { get; set; }

        public float totalPrice(){
            float total = itemDTOs
                        .Sum(i => (i.bookDTO.Price - i.bookDTO.priceDiscount * i.bookDTO.Price/100) * i.item.QuantityBooks);
            return total;
        }


    }
}
