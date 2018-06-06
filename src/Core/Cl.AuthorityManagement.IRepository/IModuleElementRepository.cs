using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IModuleElementRepository : IBaseRepository<ModuleElement>
    {
        /// <summary>
        /// 获取模块所有元素
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>元素集合</returns>
        List<ModuleElement> LoadModuleElement(int moduleID);

        /// <summary>
        /// 获取模块的所有元素ID
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>所有元素ID</returns>
        List<int> LoadModuleElementID(int moduleID);
    }
}
