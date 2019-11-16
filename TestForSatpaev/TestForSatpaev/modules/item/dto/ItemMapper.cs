using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.item.entity;

namespace TestForSatpaev.modules.item.dto
{
    public class ItemMapper
    {
        public ItemDto CastDto(Item ef)
        {
            return new ItemDto { Id = ef.Id, Name = ef.Name, Cost = ef.Cost };
        }
        public List<ItemDto> CastDto(List<Item> ef)
        {
            List<ItemDto> ret = new List<ItemDto>();
            foreach(var i in ef)
            {
                ret.Add(CastDto(i));
            }
            return ret;
        }
        public Item Cast(ItemDto dto)
        {
            return new Item { Name = dto.Name, Cost = dto.Cost };
        }
        public List<Item> Cast(List<ItemDto> dto)
        {
            List<Item> ret = new List<Item>();
            foreach (var i in dto)
            {
                ret.Add(Cast(i));
            }
            return ret;
        }
    }
}
