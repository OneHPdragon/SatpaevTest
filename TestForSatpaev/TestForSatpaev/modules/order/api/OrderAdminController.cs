using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.order.dto;
using TestForSatpaev.modules.order.service;

namespace TestForSatpaev.modules.order.api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class OrderAdminController
    {
        private OrderService orderService;
        public OrderAdminController()
        {
            orderService = new OrderService();
        }
        [HttpPost]
        public async Task<List<OrderDto>> GetOrdersByDates(TwoDatesDto dto)
        {
            return await orderService.GetOrdersByDates(dto.DateFrom, dto.DateTo);
        }
        [HttpGet("{region}")]
        public async Task<List<OrderDto>> GetOrdersByDates(string region)
        {
            return await orderService.GetOrdersByRegion(region);
        }
        [HttpGet]
        public async Task<List<OrderDto>> GetAllOrders()
        {
            return await orderService.GetAllOrders();
        }
    }
}
