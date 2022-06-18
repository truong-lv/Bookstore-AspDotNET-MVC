using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("discount")]
    public partial class Discount
    {
        public Discount()
        {
            BookDiscounts = new HashSet<BookDiscount>();
        }

        [Key]
        [Column("id_discount")]
        public long IdDiscount { get; set; }
        [Column("startTime", TypeName = "date")]
        public DateTime? StartTime { get; set; }
        [Column("endTime", TypeName = "date")]
        public DateTime? EndTime { get; set; }
        [Column("contentDiscount")]
        [StringLength(150)]
        public string ContentDiscount { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        [Column("discount_percent")]
        public int? DiscountPercent { get; set; }

        [InverseProperty(nameof(BookDiscount.IdDícountNavigation))]
        public virtual ICollection<BookDiscount> BookDiscounts { get; set; }
    }
}
