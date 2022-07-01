using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.DTO
{
    public class BookReviewPagineDTO
    {
        public List<BookReviewDTO> BookReviewDTOs{ get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        ///
        public int Status { get; set; }
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }
    }
}
