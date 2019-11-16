using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.user.entity;
using TestForSatpaev.modules.user.repository;

namespace TestForSatpaev.modules.user.service
{
    public class UserService
    {
        UserRepository userRepository;
        RoleRepository roleRepository;
        public UserService()
        {
            userRepository = new UserRepository();
            roleRepository = new RoleRepository();
        }
        public async void AddUserToRole(string userName, string roleId)
        {
            using (Context db = new Context())
            {
                User user = await userRepository.GetUserWithoutPassword(userName);
                Role role = await roleRepository.GetRole(roleId);
                if (user == null || role == null)
                    throw new Exception("Selected User or Role does not exist");
                if (user.UserRoles != null)
                {
                    foreach (var us in user.UserRoles)
                    {
                        if (us.RoleId == roleId)
                            return;
                    }
                }
                UserRole userRole = new UserRole
                {
                    User = user,
                    Role = role
                };
                db.Entry(userRole).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await db.SaveChangesAsync();
            }
        }
    }
}
