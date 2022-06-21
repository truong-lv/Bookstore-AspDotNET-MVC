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
        public long ProvinceId { get; set; }
        [Column("province_name")]
        [StringLength(100)]
        public string ProvinceName { get; set; }
        [Column("province_code")]
        [StringLength(20)]
        public string ProvinceCode { get; set; }

        [InverseProperty(nameof(District.Province))]
        public virtual ICollection<District> Districts { get; set; }
    }
}
