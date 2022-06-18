using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("province")]
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
        }

        [Key]
        [Column("province_id")]
        [StringLength(255)]
        public string ProvinceId { get; set; }
        [Column("province_name")]
        [StringLength(45)]
        public string ProvinceName { get; set; }
        [Column("province_type")]
        [StringLength(255)]
        public string ProvinceType { get; set; }

        [InverseProperty(nameof(District.Province))]
        public virtual ICollection<District> Districts { get; set; }
    }
}
