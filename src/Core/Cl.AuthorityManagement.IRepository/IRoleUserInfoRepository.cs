using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IRoleUserInfoRepository : IBaseRepository<RoleUserInfo>
    {
        List<RoleUserInfo> LoadRoleUserInfo(Expression<Func<RoleUserInfo, bool>> whereLamada);
                 
        /// <summary>
        /// 加载角色ID
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户的模块ID</returns>
        int[] LoadRoleIDs(int userID);

        /// <summary>
        /// 删除用户的角色
        /// </summary>
        /// <param name="userID">用户ID</param>
        void RemoveAll(int userID);
    }
}
