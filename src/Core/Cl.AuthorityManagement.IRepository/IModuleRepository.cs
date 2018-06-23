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
        /// 根据模块ID加载模块和元素匹配
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>模块</returns>
        Module LoadModules(int moduleID);

        /// <summary>
        /// 根据控制器名称获取模块ID
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <returns>模块ID</returns>
        int GetId(string controllerName);

        /// <summary>
        /// 加载角色模块
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色模块</returns>
        List<Module> LoadRoleModule(int roleID);
    }
}
