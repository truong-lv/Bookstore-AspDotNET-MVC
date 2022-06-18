using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("category")]
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [Column("category_id")]
        public long CategoryId { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [InverseProperty(nameof(Book.Category))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
