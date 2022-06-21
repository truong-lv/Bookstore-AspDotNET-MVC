using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("book")]
    public partial class Book
    {
        public Book()
        {
            BookDiscounts = new HashSet<BookDiscount>();
            Items = new HashSet<Item>();
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
        }

        [Key]
        [Column("id_book")]
        public long IdBook { get; set; }
        [Column("book_name")]
        [StringLength(45)]
        public string BookName { get; set; }
        [Column("describe_book", TypeName = "text")]
        public string DescribeBook { get; set; }
        [Column("picture")]
        [StringLength(255)]
        public string Picture { get; set; }
        [Column("price", TypeName = "decimal(19, 2)")]
        public float Price { get; set; }
        [Column("publish_day", TypeName = "date")]
        public DateTime? PublishDay { get; set; }
        [Column("total_quantity")]
        public int? TotalQuantity { get; set; }
        [Column("id_author")]
        public long? IdAuthor { get; set; }
        [Column("category_id")]
        public long? CategoryId { get; set; }
        [Column("id_company")]
        public long? IdCompany { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Books")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(IdAuthor))]
        [InverseProperty(nameof(Author.Books))]
        public virtual Author IdAuthorNavigation { get; set; }
        [ForeignKey(nameof(IdCompany))]
        [InverseProperty(nameof(PublishingCompany.Books))]
        public virtual PublishingCompany IdCompanyNavigation { get; set; }
        [InverseProperty(nameof(BookDiscount.IdBookNavigation))]
        public virtual ICollection<BookDiscount> BookDiscounts { get; set; }
        [InverseProperty(nameof(Item.IdBookNavigation))]
        public virtual ICollection<Item> Items { get; set; }
        [InverseProperty(nameof(OrderDetail.IdBookNavigation))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [InverseProperty(nameof(Review.IdBookNavigation))]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
