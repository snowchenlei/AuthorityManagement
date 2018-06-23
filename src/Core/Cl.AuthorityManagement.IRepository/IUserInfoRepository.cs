using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IUserInfoRepository : IBaseRepository<UserInfo>
    {
        /// <summary>
        /// 加载用户、角色、模块元素信息
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns>用户信息</returns>
        Task<List<UserInfo>> LoadUserRoleModuleElementAsync(Expression<Func<UserInfo, bool>> whereLamada);

        /// <summary>
        /// 加载用户、角色用户匹配
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>用户信息</returns>
        UserInfo LoadUserRoleUser(int userID);

        /// <summary>
        /// 加载用户、模块用户匹配
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户信息</returns>
        UserInfo LoadUserModuleUser(int userID);

        /// <summary>
        /// 加载用户模块
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户模块</returns>
        List<Module> LoadUserModule(int userID);

        /// <summary>
        /// 获取用户元素
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户元素</returns>
        List<int> LoadUserElementID(int userID);
    }
}
