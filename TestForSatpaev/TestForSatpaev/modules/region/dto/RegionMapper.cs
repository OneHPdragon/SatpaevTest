using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.region.entity;

namespace TestForSatpaev.modules.region.dto
{
    public class RegionMapper
    {
        public RegionDto CastDto(Region ef)
        {
            return new RegionDto { Id = ef.Id, Name = ef.Name, ChildRegs = (ef.ChildRegs != null && ef.ChildRegs.Count > 0) ? ef.ChildRegs.Select(x => CastDto(x)).ToList() : null };
        }

        public List<RegionDto> CastDto(List<Region> ef)
        {
            List<RegionDto> ret = new List<RegionDto>();
            foreach(Region r in ef)
            {
                ret.Add(CastDto(r));
            }
            return ret;
        }

        public Region Cast(RegionDto dto)
        {
            return new Region { Id = dto.Id, Name = dto.Name/*, ChildRegs = (dto.ChildRegs != null && dto.ChildRegs.Count > 0) ? dto.ChildRegs.Select(x => Cast(x)).ToList() : null*/ };
        }
    }
}
