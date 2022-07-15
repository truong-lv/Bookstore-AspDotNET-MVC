using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("address_detail")]
    public partial class AddressDetail
    {
        public AddressDetail()
        {
            Orders = new HashSet<Order>();
            UserAddresses = new HashSet<UserAddress>();
        }

        [Key]
        [Column("address_id")]
        public long AddressId { get; set; }
        [Column("address_name")]
        [StringLength(45)]
        public string AddressName { get; set; }
        [Column("warrd_id")]
        public long WarrdId { get; set; }

        [ForeignKey(nameof(WarrdId))]
        [InverseProperty(nameof(Ward.AddressDetails))]
        public virtual Ward Warrd { get; set; }
        [InverseProperty(nameof(Order.Address))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(UserAddress.Address))]
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
