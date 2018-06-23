using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IRoleModuleRepository:IBaseRepository<RoleModule>
    {
        /// <summary>
        /// 删除用户的模块
        /// </summary>
        /// <param name="roleID">用户ID</param>
        void RemoveAll(int roleID);
    }
}
