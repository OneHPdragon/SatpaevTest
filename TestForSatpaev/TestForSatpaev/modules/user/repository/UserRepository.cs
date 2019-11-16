using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.user.entity;

namespace TestForSatpaev.modules.user.repository
{
    public class UserRepository
    {
        //private Context db;
        //public UserRepository(Context context)
        //{
        //    db = context;
        //}
        public async Task<User> GetUser(string userName, string password)
        {
            //return await db.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.PasswordHash == password);
            //var ret = from a in db.Users
            //        join b in db.UserRoles on b.UserId equals a.Id
            using (Context db = new Context())
            {
                var ret = (from user in db.Users.Include(u => u.UserRoles)
                           where user.UserName == userName && user.PasswordHash == password
                           select user
                           ).FirstOrDefault();
                return ret;
            }
        }
        public async Task<User> GetUserWithoutPassword(string userName)
        {
            //return await db.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.PasswordHash == password);
            //var ret = from a in db.Users
            //        join b in db.UserRoles on b.UserId equals a.Id
            using (Context db = new Context())
            {
                var ret = (from user in db.Users.Include(u => u.UserRoles)
                           where user.UserName == userName
                           select user
                           ).FirstOrDefault();
                return ret;
            }
        }
        public async Task<bool> SaveUser(User user)
        {
            using (Context db = new Context())
            {
                try
                {
                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public async void AddDefaultAdmin()
        {
            //var optionsBuilder = new DbContextOptionsBuilder<Context>();
            //optionsBuilder.UseSqlServer("Server=DESKTOP-R6DTGEL\\SQLEXPRESS;Database=SatpaevTest;Trusted_Connection=True;");
            using (Context db = new Context())
            {
                modules.user.entity.User existringAdmin = await GetUser("admin", "123456");
                if (existringAdmin != null)
                    return;
                modules.user.entity.Role role = new modules.user.entity.Role();
                role.Id = "admin";
                role.Name = "Администратор";
                db.Entry(role).State = EntityState.Added;

                modules.user.entity.User user = new modules.user.entity.User();
                user.UserName = "admin";
                user.PasswordHash = "123456";
                db.Entry(user).State = EntityState.Added;

                Role userRole1 = new Role { Id = "user", Name = "Пользователь" };
                db.Entry(userRole1).State = EntityState.Added;
                db.SaveChanges();

                db.Entry(role).Reload();
                db.Entry(user).Reload();
                modules.user.entity.UserRole userRole = new modules.user.entity.UserRole();
                userRole.User = user;
                userRole.Role = role;
                db.Entry(userRole).State = EntityState.Added;



                db.SaveChanges();
            }
        }
    }
}
