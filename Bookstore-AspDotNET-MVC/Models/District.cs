using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("district")]
    public partial class District
    {
        public District()
        {
            Villages = new HashSet<Village>();
        }

        [Key]
        [Column("district_id")]
        [StringLength(255)]
        public string DistrictId { get; set; }
        [Column("district_name")]
        [StringLength(45)]
        public string DistrictName { get; set; }
        [Column("district_type")]
        [StringLength(255)]
        public string DistrictType { get; set; }
        [Column("province_id")]
        [StringLength(255)]
        public string ProvinceId { get; set; }

        [ForeignKey(nameof(ProvinceId))]
        [InverseProperty("Districts")]
        public virtual Province Province { get; set; }
        [InverseProperty(nameof(Village.District))]
        public virtual ICollection<Village> Villages { get; set; }
    }
}
