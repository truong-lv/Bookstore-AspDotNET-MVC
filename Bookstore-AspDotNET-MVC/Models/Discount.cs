using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("discount")]
    public partial class Discount
    {
        public Discount()
        {
            BookDiscounts = new HashSet<BookDiscount>();
            StartTime = DateTime.Now;
            EndTime = StartTime.AddDays(7);
            Status = 1;
        }

        [Key]
        [Column("id_discount")]
        public long IdDiscount { get; set; }
        [Column("startTime", TypeName = "date")]

        [Required(ErrorMessage = "Ngày áp dụng không được để trống !!")]
        [DisplayName("Từ")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc áp dụng không được để trống !!")]
        [DisplayName("Đến")]
        [Column("endTime", TypeName = "date")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Nộ dung áp dụng không được để trống !!")]
        [DisplayName("Nội dung")]
        [Column("contentDiscount")]
        [StringLength(150)]
        public string ContentDiscount { get; set; }

        [Range(minimum: 0, maximum: 1,ErrorMessage = "Trạng thái không hợp lệ !!")]
        [DisplayName("Trạng thái: 1- Áp dụng, 0- Khóa")]
        [Column("status")]
        public int Status { get; set; }

        [Range(minimum: 0, maximum: 100, ErrorMessage = "Giá trị khuyến mãi không hợp lệ [1,100] !!")]
        [DisplayName("Giá trị(%)")]
        [Column("discount_percent")]
        public int DiscountPercent { get; set; }

        [InverseProperty(nameof(BookDiscount.IdDiscountNavigation))]
        public virtual ICollection<BookDiscount> BookDiscounts { get; set; }
    }
}
