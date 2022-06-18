using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("user_address")]
    public partial class UserAddress
    {
        [Key]
        [Column("address_id")]
        public long AddressId { get; set; }
        [Key]
        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty(nameof(AddressDetail.UserAddresses))]
        public virtual AddressDetail Address { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Userinfor.UserAddresses))]
        public virtual Userinfor User { get; set; }
    }
}
