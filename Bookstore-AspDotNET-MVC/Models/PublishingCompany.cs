using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("publishing_company")]
    public partial class PublishingCompany
    {
        public PublishingCompany()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [Column("id_company")]
        public long IdCompany { get; set; }
        [Column("company_name")]
        [StringLength(45)]
        public string CompanyName { get; set; }

        [InverseProperty(nameof(Book.IdCompanyNavigation))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
