using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("userinfor")]
    public partial class Userinfor
    {
        public Userinfor()
        {
            Items = new HashSet<Item>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            UserAddresses = new HashSet<UserAddress>();
            UserRoles = new HashSet<UserRole>();
        }

        [Key]
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [Column("gender")]
        [MaxLength(1)]
        public bool Gender { get; set; }
        [Column("password")]
        [StringLength(250)]
        public string Password { get; set; }
        [Column("phone")]
        [StringLength(10)]
        public string Phone { get; set; }
        [Column("username")]
        [StringLength(255)]
        public string Username { get; set; }

        [InverseProperty(nameof(Item.User))]
        public virtual ICollection<Item> Items { get; set; }
        [InverseProperty(nameof(Order.User))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(Review.User))]
        public virtual ICollection<Review> Reviews { get; set; }
        [InverseProperty(nameof(UserAddress.User))]
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        [InverseProperty(nameof(UserRole.User))]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
