using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cl.AuthorityManagement.Repository
{
    public partial class RoleModuleRepository:BaseRepository<RoleModule>, IRoleModuleRepository
    {
        /// <summary>
        /// 删除用户的模块
        /// </summary>
        /// <param name="roleID">用户ID</param>
        public void RemoveAll(int roleID)
        {
            var moduleUsers = CurrentContext.RoleModule
                .Where(m => m.RoleID == roleID);

            CurrentContext.RoleModule
                .RemoveRange(moduleUsers);
        }       
    }
}
