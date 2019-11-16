using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.item.dto;
using TestForSatpaev.modules.item.entity;

namespace TestForSatpaev.modules.item.repository
{
    public class ItemRepository
    {
        ItemMapper itemMapper;
        public ItemRepository()
        {
            itemMapper = new ItemMapper();
        }
        public async Task<bool> SaveItem(ItemDto dto)
        {
            using (Context db = new Context())
            {
                Item item = itemMapper.Cast(dto);
                db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await db.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> UpdateItem(ItemDto dto)
        {
            using(Context db = new Context())
            {
                Item item = await db.Items.FirstOrDefaultAsync(x => x.Name == dto.Name);
                if (item == null)
                    throw new Exception("Item not found");
                item = itemMapper.Cast(dto);
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
        }
        public async Task<Item> GetItemEntity(string name)
        {
            using (Context db = new Context())
            {
                Item item = await db.Items.FirstOrDefaultAsync(x => x.Name == name);
                if (item == null)
                    throw new Exception("Item not found");
                return item;
            }
        }
        public async Task<ItemDto> GetItem(string name)
        {
            using(Context db = new Context())
            {
                Item item = await db.Items.FirstOrDefaultAsync(x => x.Name == name);
                if (item == null)
                    throw new Exception("Item not found");
                return itemMapper.CastDto(item);
            }
        }
        public async Task<List<ItemDto>> GetAllItems()
        {
            using (Context db = new Context())
            {
                List<Item> item = await db.Items.ToListAsync();
                return itemMapper.CastDto(item);
            }
        }
        public async Task<bool> DeleteItem(string name)
        {
            using(Context db = new Context())
            {
                Item item = await db.Items.FirstOrDefaultAsync(x => x.Name == name);
                if (item == null)
                    return true;
                db.Entry(item).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
