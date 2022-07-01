using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.DTO
{
    public class StarDTO
    {
        public StarDTO(int star,int count)
        {
            this.star = star;
            this.count = count;

        }
        public int star { get; set; }

        public int count { get; set; }
    }
}
