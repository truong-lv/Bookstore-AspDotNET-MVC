using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("roles")]
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [Key]
        [Column("id_role")]
        public long IdRole { get; set; }
        [Column("role_name")]
        [StringLength(45)]
        public string RoleName { get; set; }

        [InverseProperty(nameof(UserRole.IdRoleNavigation))]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
