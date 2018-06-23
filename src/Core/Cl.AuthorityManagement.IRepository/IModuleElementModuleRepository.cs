using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IModuleElementModuleRepository:IBaseRepository<ModuleElementModule>
    {
        /// <summary>
        /// 加载元素ID
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>用户的模块ID</returns>
        int[] LoadElementIDs(int moduleID);

        /// <summary>
        /// 删除模块元素的模块
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        void RemoveAll(int moduleID);
    }
}
