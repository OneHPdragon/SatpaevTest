using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.item.dto;
using TestForSatpaev.modules.item.repository;

namespace TestForSatpaev.modules.item.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private ItemRepository itemRepository;
        public ItemController()
        {
            itemRepository = new ItemRepository();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<bool> SaveItem(ItemDto dto)
        {
            return await itemRepository.SaveItem(dto);
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<bool> UpdateItem(ItemDto dto)
        {
            return await itemRepository.UpdateItem(dto);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteItem(string id)
        {
            return await itemRepository.DeleteItem(id);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<List<ItemDto>> GetAllItems()
        {
            return await itemRepository.GetAllItems();
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ItemDto> GetItem(string id)
        {
            return await itemRepository.GetItem(id);
        }
    }
}
