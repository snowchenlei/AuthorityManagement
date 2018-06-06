using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.Data
{
    public class DbInitializer
    {
        public static void Initialize(AuthorityManagementContext context)
        {
            context.Database.EnsureCreated();
            UserInfo user = new Entity.UserInfo
            {
                IsCanUse = true,
                Name = "测试",
                Password = "1",
                PhoneNumber = "13442567835",
                UserName = "test",
                AddTime = DateTime.Now
            };
            Role role = new Role
            {
                Name = "测试",
                Sort = 1,
                AddTime = DateTime.Now
            };
            context.UserInfo.Add(user);
            context.Role.Add(role);
            context.RoleUserInfo.Add(new RoleUserInfo { Role = role, UserInfo = user });

            context.SaveChanges();
        }
    }
}
