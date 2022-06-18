using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Models
{
    [Table("village")]
    public partial class Village
    {
        public Village()
        {
            AddressDetails = new HashSet<AddressDetail>();
        }

        [Key]
        [Column("village_id")]
        [StringLength(255)]
        public string VillageId { get; set; }
        [Column("village_name")]
        [StringLength(45)]
        public string VillageName { get; set; }
        [Column("village_type")]
        [StringLength(255)]
        public string VillageType { get; set; }
        [Column("district_id")]
        [StringLength(255)]
        public string DistrictId { get; set; }

        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("Villages")]
        public virtual District District { get; set; }
        [InverseProperty(nameof(AddressDetail.Village))]
        public virtual ICollection<AddressDetail> AddressDetails { get; set; }
    }
}
