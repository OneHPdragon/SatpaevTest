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
    public class OrderPageController
    {
        private OrderService orderService;
        public OrderPageController()
        {
            orderService = new OrderService();
        }

        [HttpGet("{page}")]
        public async Task<List<OrderDto>> GetOrderByPage(int page)
        {
            return await orderService.GetOrdersByPage(page);
        }
    }
}
