using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("payment")]
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("id_payment")]
        public long IdPayment { get; set; }
        [Column("payment_type")]
        [StringLength(50)]
        public string PaymentType { get; set; }
        [Column("payment_status")]
        public int? PaymentStatus { get; set; }

        [InverseProperty(nameof(Order.IdPaymentNavigation))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
