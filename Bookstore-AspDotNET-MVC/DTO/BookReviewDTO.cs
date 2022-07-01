using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.DTO
{
    public class BookReviewDTO
    {
        public Book Book { get; set; }


        public List<StarDTO> ListStart { get; set; }

    }
}
