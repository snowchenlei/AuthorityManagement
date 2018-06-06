using Cl.AuthorityManagement.Entity;
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
        /// 设置角色模块
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="moduleIds">模块id集合</param>
        /// <returns>是否成功</returns>
        bool SetRoleModule(Role role, int[] moduleIds);

        /// <summary>
        /// 设置角色模块元素
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="elementIds">元素id集合</param>
        /// <param name="moduleId">模块id</param>
        /// <returns>是否成功</returns>
        bool SetRoleModuleElements(Role role, int[] elementIds, int moduleId);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">要删除的角色编号</param>
        /// <returns>是否成功</returns>
        bool Delete(int roleId);
    }
}
