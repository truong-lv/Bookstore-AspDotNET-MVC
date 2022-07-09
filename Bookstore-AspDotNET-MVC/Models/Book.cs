using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            PublishDay = DateTime.Now;
        }

        [Key]
        [Column("id_book")]
        public long IdBook { get; set; }

        [Required(ErrorMessage ="Tên sách không được để trống !!")]
        [DisplayName("Tên sách")]
        [Column("book_name")]
        [StringLength(45)]
        public string BookName { get; set; }

        [DisplayName("Mô tả")]
        [Column("describe_book", TypeName = "nvarchar(MAX)")]
        public string DescribeBook { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        [DisplayName("Ảnh")]
        [Column("picture")]
        [StringLength(255)]
        public string Picture { get; set; }

        [Range(minimum: 1, maximum: 10000000,ErrorMessage = " Giá phải lớn hơn 0")]
        [DisplayName("Giá sách")]
        [Column("price", TypeName = "decimal(19, 2)")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Ngày không được để trống !!")]
        [DisplayName("Ngày xuất bản")]
        [Column("publish_day", TypeName = "date")]
        public DateTime PublishDay { get; set; }

        [Range(minimum: 1, maximum: 1000, ErrorMessage = " Số lượng phải lớn hơn 0")]
        [DisplayName("Số lượng sách")]
        [Column("total_quantity")]
        public int TotalQuantity { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống !!")]
        [DisplayName("Tác giả")]
        [Column("id_author")]
        public long IdAuthor { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống !!")]
        [DisplayName("Thể loại")]
        [Column("category_id")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống !!")]
        [DisplayName("Nhà xuất bản")]
        [Column("id_company")]
        public long IdCompany { get; set; }

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
