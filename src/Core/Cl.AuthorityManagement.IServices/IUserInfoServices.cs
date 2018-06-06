using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IServices
{
    public partial interface IUserInfoServices:IBaseServices<UserInfo>
    {
        Task<UserInfo> LoadUserInfoAsync(Expression<Func<UserInfo, bool>> whereLamada);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="user">要设置的用户</param>
        /// <param name="roleIds">角色Id集合</param>
        /// <returns>是否成功</returns>
        bool SetUserRole(UserInfo user, int[] roleIds);

        /// <summary>
        /// 设置用户模块
        /// </summary>
        /// <param name="user">要设置的用户</param>
        /// <param name="roleIds">模块Id集合</param>
        /// <returns>是否成功</returns>
        bool SetUserModule(UserInfo user, int[] moduleIds);

        /// <summary>
        /// 设置用户模块元素
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="elementIds">元素id集合</param>
        /// <param name="moduleId">模块id</param>
        /// <returns>是否成功</returns>
        bool SetUserModuleElements(UserInfo user, int[] elementIds, int moduleId);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">要删除的用户编号</param>
        /// <returns>是否成功</returns>
        bool Delete(int userId);
    }
}
