using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("items")]
    public partial class Item
    {
        [Key]
        [Column("item_id")]
        public long ItemId { get; set; }
        [Column("quantity_books")]
        public int QuantityBooks { get; set; }
        [Column("id_book")]
        public long IdBook { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey(nameof(IdBook))]
        [InverseProperty(nameof(Book.Items))]
        public virtual Book IdBookNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Userinfor.Items))]
        public virtual Userinfor User { get; set; }
    }
}
