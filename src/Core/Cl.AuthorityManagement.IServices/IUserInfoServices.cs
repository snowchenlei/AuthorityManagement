using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Model;
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
        /// <summary>
        /// 加载用户、角色、模块元素信息
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns>用户信息</returns>
        Task<List<UserInfo>> LoadUserRoleModuleElementAsync(Expression<Func<UserInfo, bool>> whereLamada);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        CheckReturn LoadRoles(int userID);

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        CheckReturn LoadModules(int userID);

        /// <summary>
        /// 获取模块元素
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="moduleID">模块ID</param>
        /// <returns>模块元素</returns>
        CheckReturn LoadModuleElements(int userID, int moduleID);

        /// <summary>
        /// 获取用户模块
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户模块</returns>
        List<Module> LoadUserModule(int userID);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="roleIds">角色Id集合</param>
        /// <returns>是否成功</returns>
        ReturnDescription SetUserRole(int userID, int[] roleIds);

        /// <summary>
        /// 设置用户模块
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="moduleIDs">选中模块Id集合</param>
        /// <returns>是否成功</returns>
        ReturnDescription SetUserModule(int userID, int[] moduleIDs);

        /// <summary>
        /// 设置用户模块元素
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="elementIDs">模块元素ID集合</param>
        /// <param name="moduleID">模块ID</param>
        /// <returns>结果描述</returns>
        ReturnDescription SetUserModuleElements(int userID, int[] elementIDs, int moduleID);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">要删除的用户编号</param>
        /// <returns>是否成功</returns>
        bool Delete(int userId);
    }
}
