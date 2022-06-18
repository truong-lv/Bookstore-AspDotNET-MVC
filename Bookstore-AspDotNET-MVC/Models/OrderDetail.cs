using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("order_detail")]
    public partial class OrderDetail
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("price", TypeName = "decimal(19, 2)")]
        public decimal? Price { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("id_book")]
        public long? IdBook { get; set; }
        [Column("order_id")]
        public long? OrderId { get; set; }

        [ForeignKey(nameof(IdBook))]
        [InverseProperty(nameof(Book.OrderDetails))]
        public virtual Book IdBookNavigation { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; }
    }
}
