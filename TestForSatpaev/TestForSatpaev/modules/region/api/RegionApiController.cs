using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.region.dto;
using TestForSatpaev.modules.region.service;

namespace TestForSatpaev.modules.region.api
{
    //[Authorize]
    //[Route("api/region")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        RegionService regionService;
        public RegionController()
        {
            regionService = new RegionService();
        }

        [Authorize(Roles = "admin")]
        [HttpPost/*("/saveRegion")*/]
        public async Task<bool> SaveRegion([FromBody] RegionDto dto)
        {
            return await regionService.SaveRegion(dto);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<RegionDto> GetRegion(string id)
        {
            return await regionService.GetRegion(id);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<List<RegionDto>> GetRegion()
        {
            return await regionService.GetAllRegions();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteRegion(string id)
        {
            return await regionService.DeleteRegion(id);
        }
    }
}
