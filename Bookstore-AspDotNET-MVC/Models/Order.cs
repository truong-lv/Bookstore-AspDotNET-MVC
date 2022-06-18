using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("orders")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [Column("order_id")]
        public long OrderId { get; set; }
        [Column("name_of_customer")]
        [StringLength(45)]
        public string NameOfCustomer { get; set; }
        [Column("order_day", TypeName = "date")]
        public DateTime? OrderDay { get; set; }
        [Column("order_status")]
        public int? OrderStatus { get; set; }
        [Column("phone_of_customer")]
        [StringLength(10)]
        public string PhoneOfCustomer { get; set; }
        [Column("total_price", TypeName = "decimal(19, 2)")]
        public decimal? TotalPrice { get; set; }
        [Column("address_id")]
        public long? AddressId { get; set; }
        [Column("user_id")]
        public long? UserId { get; set; }
        [Column("id_payment")]
        public long? IdPayment { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty(nameof(AddressDetail.Orders))]
        public virtual AddressDetail Address { get; set; }
        [ForeignKey(nameof(IdPayment))]
        [InverseProperty(nameof(Payment.Orders))]
        public virtual Payment IdPaymentNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Userinfor.Orders))]
        public virtual Userinfor User { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
