using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestForSatpaev.modules.user.entity
{
    [Table("Roles")]
    public class Role
    {
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        
        public IList<UserRole> UserRoles { get; set; }
    }
}
