using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("review")]
    public partial class Review
    {
        [Key]
        [Column("id_book")]
        public long IdBook { get; set; }
        [Key]
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("comments")]
        [StringLength(200)]
        public string Comments { get; set; }
        [Column("star")]
        public int Star { get; set; }
        [Column("time", TypeName = "datetime")]
        public DateTime? Time { get; set; }

        [ForeignKey(nameof(IdBook))]
        [InverseProperty(nameof(Book.Reviews))]
        public virtual Book IdBookNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Userinfor.Reviews))]
        public virtual Userinfor User { get; set; }
    }
}
