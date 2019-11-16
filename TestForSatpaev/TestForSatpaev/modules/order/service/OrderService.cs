using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.order.dto;
using TestForSatpaev.modules.order.repository;
using TestForSatpaev.modules.region.entity;
using TestForSatpaev.modules.region.repository;

namespace TestForSatpaev.modules.order.service
{
    public class OrderService
    {
        private OrderRepository orderRepository;
        private RegionRepository regionRepository;
        private OrderMapper orderMapper;
        public OrderService()
        {
            orderRepository = new OrderRepository();
            regionRepository = new RegionRepository();
            orderMapper = new OrderMapper();
        }
        public async Task<bool> SaveOrder(OrderDto dto)
        {
            return await orderRepository.SaveOrder(dto);
        }
        public async Task<List<OrderDto>> GetOrdersByPage(int page)
        {
            return orderMapper.CastDto(await orderRepository.GetOrdersByPage(page, 5));
        }
        public async Task<List<OrderDto>> GetOrdersByDates(DateTime dateFrom, DateTime dateTo)
        {
            return orderMapper.CastDto(await orderRepository.GetOrdersByDates(dateFrom, dateTo));
        }
        public async Task<List<OrderDto>> GetOrdersByRegion(string regionName)
        {
            Region region = await regionRepository.GetRegionWithoutChilds(regionName);
            if (region == null)
                throw new Exception("Region does not exists");
            return orderMapper.CastDto(await orderRepository.GetOrdersByRegion(region));
        }
        public async Task<List<OrderDto>> GetAllOrders()
        {
            return orderMapper.CastDto(await orderRepository.GetAllOrders());
        }
    }
}
