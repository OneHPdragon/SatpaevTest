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
    [Authorize(Roles = "user")]
    [ApiController]
    public class OrderUserController
    {
        private OrderService orderService;
        public OrderUserController()
        {
            orderService = new OrderService();
        }
        public async Task<bool> SaveOrder(OrderDto dto)
        {
            return await orderService.SaveOrder(dto);
        }
    }
}
