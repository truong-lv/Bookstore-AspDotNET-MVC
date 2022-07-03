using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("book_discount")]
    public partial class BookDiscount
    {
        [Key]
        [Column("id_book")]
        [DisplayName("Tên sách")]
        public long IdBook { get; set; }
        [Key]
        [Column("id_discount")]
        public long IdDiscount { get; set; }

        [ForeignKey(nameof(IdBook))]
        [InverseProperty(nameof(Book.BookDiscounts))]
        public virtual Book IdBookNavigation { get; set; }
        [ForeignKey(nameof(IdDiscount))]
        [InverseProperty(nameof(Discount.BookDiscounts))]
        public virtual Discount IdDiscountNavigation { get; set; }
    }
}
