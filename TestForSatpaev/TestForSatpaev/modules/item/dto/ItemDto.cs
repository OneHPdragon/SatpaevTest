using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForSatpaev.modules.item.dto
{
    public class ItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public double Cost { get; set; }
    }
}
