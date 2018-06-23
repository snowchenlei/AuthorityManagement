using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Repository
{
    public partial class RoleUserInfoRepository : BaseRepository<RoleUserInfo>, IRoleUserInfoRepository
    {
        public RoleUserInfoRepository(AuthorityManagementContext context) : base(context)
        {
        }

        public List<RoleUserInfo> LoadRoleUserInfo(Expression<Func<RoleUserInfo, bool>> whereLamada)
        {
            return CurrentContext.RoleUserInfo
                .Include(i => i.Role)
                    .ThenInclude(i => i.RoleModules)
                        .ThenInclude(i => i.Module)
                .AsNoTracking()
                .Where(whereLamada)
                .ToList();
        }

        /// <summary>
        /// 加载角色ID
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户的模块ID</returns>
        public int[] LoadRoleIDs(int userID)
        {
            return CurrentContext.RoleUserInfo
                .AsNoTracking()
                .Where(w => w.UserInfoID == userID)
                .Select(s => s.RoleID)
                .ToArray();
        }

        /// <summary>
        /// 删除用户的角色
        /// </summary>
        /// <param name="userID">用户ID</param>
        public void RemoveAll(int userID)
        {
            var roleUsers = CurrentContext.RoleUserInfo
                .Where(m => m.UserInfoID == userID);

            CurrentContext.RoleUserInfo
                .RemoveRange(roleUsers);
        }

    }
}
