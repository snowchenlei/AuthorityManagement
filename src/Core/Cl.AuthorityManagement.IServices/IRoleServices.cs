using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IServices
{
    public partial interface IRoleServices:IBaseServices<Role>
    {
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色模块列表</returns>
        CheckReturn LoadModules(int roleID);

        /// <summary>
        /// 加载角色模块
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色模块</returns>
        List<Module> LoadRoleModule(int roleID);

        /// <summary>
        /// 获取模块元素列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="moduleID">模块ID</param>
        /// <returns>角色模块元素列表</returns>
        CheckReturn LoadModuleElements(int roleID, int moduleID);

        /// <summary>
        /// 设置用户模块
        /// </summary>
        /// <param name="roleID">用户ID</param>
        /// <param name="moduleIDs">选中模块Id集合</param>
        /// <returns>结果描述</returns>
        ReturnDescription SetRoleModule(int roleID, int[] moduleIDs);

        /// <summary>
        /// 设置用户模块
        /// </summary>
        /// <param name="roleID">用户ID</param>
        /// <param name="moduleElementIDs">选中模块Id集合</param>
        /// <returns>结果描述</returns>
        ReturnDescription SetRoleModuleElements(int roleID, int[] moduleElementIDs, int moduleID);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">要删除的角色编号</param>
        /// <returns>是否成功</returns>
        bool Delete(int roleId);
    }
}
