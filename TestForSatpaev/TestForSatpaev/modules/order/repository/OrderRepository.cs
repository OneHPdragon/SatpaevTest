using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.item.entity;
using TestForSatpaev.modules.item.repository;
using TestForSatpaev.modules.order.dto;
using TestForSatpaev.modules.order.entity;
using TestForSatpaev.modules.region.entity;
using TestForSatpaev.modules.region.repository;

namespace TestForSatpaev.modules.order.repository
{
    public class OrderRepository
    {
        RegionRepository regionRepository;
        ItemRepository itemRepository;
        public OrderRepository()
        {
            regionRepository = new RegionRepository();
            itemRepository = new ItemRepository();
        }
        public async Task<bool> SaveOrder(OrderDto dto)
        {
            using (Context db = new Context())
            {
                Region region = await regionRepository.GetRegionWithoutChilds(dto.RegionName);
                Item item = await itemRepository.GetItemEntity(dto.ItemName);
                if (region == null || item == null)
                    throw new Exception("Region or Item does not exist");
                Order order = new Order { Date = dto.Date, ItemQuantity = dto.ItemQuantity, Item = item, Region = region };
                db.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await db.SaveChangesAsync();
                return true;
            }
        }
        public async Task<List<Order>> GetOrdersByPage(int page, int pageSize)
        {
            using (Context db = new Context())
            {
                IQueryable<Order> orders = db.Orders.Include(x => x.Item).Include(x => x.Region);
                var count = await orders.CountAsync();
                var items = await orders.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return items;
            }
        }
        public async Task<List<Order>> GetOrdersByDates(DateTime fromDate, DateTime toDate)
        {
            using(Context db = new Context())
            {
                List <Order> orders = await (from order in db.Orders.Include(x => x.Region).Include(x => x.Item)
                                             where order.Date <= toDate && order.Date >= fromDate
                                             select order).ToListAsync();
                return orders;
            }
        }
        public async Task<List<Order>> GetOrdersByRegion(Region region)
        {
            using (Context db = new Context())
            {
                List<Order> orders = await (from order in db.Orders.Include(x => x.Region).Include(x => x.Item)
                                            where order.RegionId == region.Id
                                            select order).ToListAsync();
                return orders;
            }
        }
        public async Task<List<Order>> GetAllOrders()
        {
            using(Context db = new Context())
            {
                List<Order> orders = await (from order in db.Orders.Include(x => x.Region).Include(x => x.Item)
                                            select order).ToListAsync();
                return orders;
            }
        }
    }
}
