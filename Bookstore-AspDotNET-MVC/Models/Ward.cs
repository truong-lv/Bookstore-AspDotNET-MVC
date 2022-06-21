using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("ward")]
    public partial class Ward
    {
        public Ward()
        {
            AddressDetails = new HashSet<AddressDetail>();
        }

        [Key]
        [Column("ward_id")]
        public long WardId { get; set; }
        [Required]
        [Column("ward_name")]
        [StringLength(50)]
        public string WardName { get; set; }
        [Column("ward_prefix")]
        [StringLength(20)]
        public string WardPrefix { get; set; }
        [Column("district_id")]
        public long? DistrictId { get; set; }

        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("Wards")]
        public virtual District District { get; set; }
        [InverseProperty(nameof(AddressDetail.Warrd))]
        public virtual ICollection<AddressDetail> AddressDetails { get; set; }
    }
}
