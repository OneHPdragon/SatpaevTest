using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForSatpaev.modules.region.dto
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<RegionDto> ChildRegs { get; set; }
    }
}
