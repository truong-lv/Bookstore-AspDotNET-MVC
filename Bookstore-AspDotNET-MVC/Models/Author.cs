using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("author")]
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [Column("id_author")]
        public long IdAuthor { get; set; }
        [Column("author_name")]
        [StringLength(50)]
        public string AuthorName { get; set; }

        [InverseProperty(nameof(Book.IdAuthorNavigation))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
