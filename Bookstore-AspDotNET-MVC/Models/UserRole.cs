using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("user_role")]
    public partial class UserRole
    {
        [Key]
        [Column("user_role_id")]
        public long UserRoleId { get; set; }
        [Column("id_role")]
        public long? IdRole { get; set; }
        [Column("user_id")]
        public long? UserId { get; set; }

        [ForeignKey(nameof(IdRole))]
        [InverseProperty(nameof(Role.UserRoles))]
        public virtual Role IdRoleNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Userinfor.UserRoles))]
        public virtual Userinfor User { get; set; }
    }
}
