using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cl.AuthorityManagement.Repository
{
    public partial class RoleModuleElementRepository:BaseRepository<RoleModuleElement>, IRoleModuleElementRepository
    {
        /// <summary>
        /// 删除角色的模块
        /// </summary>
        /// <param name="roleID">角色 ID</param>
        /// <param name="moduleID">模块ID</param>
        public void RemoveAll(int roleID, int moduleID)
        {
            var moduleUsers = CurrentContext.RoleModuleElement
                .Where(m => m.RoleID == roleID && m.ModuleID == moduleID);

            CurrentContext.RoleModuleElement
                .RemoveRange(moduleUsers);
        }
    }
}
