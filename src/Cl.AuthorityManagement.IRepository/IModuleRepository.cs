using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IModuleRepository : IBaseRepository<Module>
    {
        /// <summary>
        /// 根据控制器名称获取模块ID
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <returns>模块ID</returns>
        int GetId(string controllerName);
    }
}
