using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("district")]
    [Index(nameof(ProvinceId), Name = "_province_id")]
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        [Key]
        [Column("district_id")]
        public long DistrictId { get; set; }
        [Column("district_name")]
        [StringLength(100)]
        public string DistrictName { get; set; }
        [Column("district_prefix")]
        [StringLength(100)]
        public string DistrictPrefix { get; set; }
        [Column("province_id")]
        public long? ProvinceId { get; set; }

        [ForeignKey(nameof(ProvinceId))]
        [InverseProperty("Districts")]
        public virtual Province Province { get; set; }
        [InverseProperty(nameof(Ward.District))]
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
