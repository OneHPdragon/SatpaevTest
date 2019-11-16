using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestForSatpaev.modules.region.entity;
using TestForSatpaev.modules.item.entity;

namespace TestForSatpaev.modules.order.entity
{
    [Table("Orders")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public int ItemQuantity { get; set; }

        public Guid RegionId { get; set; }
        [ForeignKey("RegionId")]
        public Region Region { get; set; }

        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
    }
}
