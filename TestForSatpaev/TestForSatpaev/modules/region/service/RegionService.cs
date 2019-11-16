using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.region.dto;
using TestForSatpaev.modules.region.entity;
using TestForSatpaev.modules.region.repository;

namespace TestForSatpaev.modules.region.service
{
    public class RegionService
    {
        private RegionRepository regionRepository;
        private RegionMapper regionMapper;
        public RegionService()
        {
            regionRepository = new RegionRepository();
            regionMapper = new RegionMapper();
        }
        public async Task<bool> SaveRegion(RegionDto dto)
        {
            async Task<Region> SaveRegsWithChilds(RegionDto reg)
            {
                List<Region> regs = new List<Region>();
                if(reg.ChildRegs == null || reg.ChildRegs.Count == 0)
                {
                    return await regionRepository.SaveSingleRegion(reg);
                }
                else
                {
                    foreach (RegionDto rd in reg.ChildRegs)
                        regs.Add(await SaveRegsWithChilds(rd));
                }
                return await regionRepository.SaveRegionWithChilds(reg, regs);
            }
            await SaveRegsWithChilds(dto);
            return true;
        }
        public async Task<RegionDto> GetRegion(string name)
        {
            return regionMapper.CastDto(await regionRepository.GetRegionWithChilds(name));
        } 
        public async Task<List<RegionDto>> GetAllRegions()
        {
            return regionMapper.CastDto(await regionRepository.GetAllRegions());
        }
        public async Task<bool> DeleteRegion(string name)
        {
            Region region = await regionRepository.GetRegionWithChilds(name);
            if (region == null)
                throw new Exception("No such region");
            async Task<bool> DeleteRegWithChilds(Region reg)
            {
                if (reg.ChildRegs == null || reg.ChildRegs.Count == 0)
                    return await regionRepository.Delete(reg.Name);
                else
                {
                    foreach(Region r in reg.ChildRegs)
                        await DeleteRegWithChilds(r);
                    return await regionRepository.Delete(reg.Name);
                }
            }
            return await DeleteRegWithChilds(region);
        }
    }
}
