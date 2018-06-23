using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IRoleModuleElementRepository
    {
        /// <summary>
        /// 删除角色的模块
        /// </summary>
        /// <param name="roleID">角色 ID</param>
        /// <param name="moduleID">模块ID</param>
        void RemoveAll(int roleID, int moduleID);
    }
}
