using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.order.entity;

namespace TestForSatpaev.modules.order.dto
{
    public class OrderMapper
    {
        public List<OrderDto> CastDto(List<Order> ef)
        {
            List<OrderDto> ret = new List<OrderDto>();
            foreach(Order i in ef)
            {
                ret.Add(CastDto(i));
            }
            return ret;
        }
        public OrderDto CastDto(Order ef)
        {
            return new OrderDto { Date = ef.Date, ItemQuantity = ef.ItemQuantity, ItemName = ef.Item.Name, RegionName = ef.Region.Name };
        }
    }
}
