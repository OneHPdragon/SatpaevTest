using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.user.entity;

namespace TestForSatpaev.modules.user.repository
{
    public class RoleRepository
    {
        public async Task<Role> GetRole(string id)
        {
            using(Context db = new Context())
            {
                return await (from role in db.Roles
                        where role.Id == id
                        select role).FirstOrDefaultAsync();
            }
        }
    }
}
