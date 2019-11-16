using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.item.dto;
using TestForSatpaev.modules.region.dto;

namespace TestForSatpaev.modules.order.dto
{
    public class OrderDto
    {
        public DateTime Date { get; set; }
        public int ItemQuantity { get; set; }
        
        public string RegionName { get; set; }
        
        public string ItemName { get; set; }
    }
}
