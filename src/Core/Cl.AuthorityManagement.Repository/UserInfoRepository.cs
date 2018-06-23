using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Repository
{
    public partial class UserInfoRepository: BaseRepository<UserInfo>, IUserInfoRepository
    {
        /// <summary>
        /// 加载用户、角色、模块元素信息
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns>用户信息</returns>
        public Task<List<UserInfo>> LoadUserRoleModuleElementAsync(Expression<Func<UserInfo, bool>> whereLamada)
        {
            return CurrentContext.UserInfo
                .Include(i => i.RoleUserInfos)
                    .ThenInclude(i => i.Role)
                .Include(i => i.UserInfoModuleElements)
                    .ThenInclude(i => i.ModuleElement)
                .Include(i => i.UserInfoModuleElements)
                    .ThenInclude(i => i.Module)
                .AsNoTracking()
                .Where(whereLamada)
                .ToListAsync();
        }

        /// <summary>
        /// 加载用户、角色用户匹配
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户信息</returns>
        public UserInfo LoadUserRoleUser(int userID)
        {
            return CurrentContext.UserInfo
                .Include(i => i.RoleUserInfos)
                .AsNoTracking()
                .SingleOrDefault(u => u.ID == userID);
        }

        /// <summary>
        /// 加载用户、模块用户匹配
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户信息</returns>
        public UserInfo LoadUserModuleUser(int userID)
        {
            return CurrentContext.UserInfo
                .Include(i => i.ModuleUserInfos)
                .AsNoTracking()
                .SingleOrDefault(u => u.ID == userID);
        }

        /// <summary>
        /// 加载用户模块
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户模块</returns>
        public List<Module> LoadUserModule(int userID)
        {
            //List<Module> modules = CurrentContext.ModuleUserInfo

            return CurrentContext.Module
                .FromSql(@"SELECT distinct m.*
                           FROM[UserInfo] AS[u]
                           LEFT JOIN[ModuleUserInfo] AS[mu] ON[u].[ID] = [mu].[UserInfoID]
                           LEFT JOIN[RoleUserInfo] AS[ru] ON[u].[ID] = [ru].[UserInfoID]
                           LEFT JOIN[RoleModule] AS[rm] ON[ru].[RoleID] = [rm].[RoleID]
                           RIGHT JOIN[Module] AS m ON[rm].[ModuleID] = m.[ID] OR mu.ModuleID = m.ID
                           WHERE[u].[ID] = {0}", userID)
                .ToList();
        }

        /// <summary>
        /// 获取用户元素
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户元素</returns>
        public List<int> LoadUserElementID(int userID)
        {
            return CurrentContext.ModuleElement
                .FromSql(@"SELECT distinct e.*
                            FROM[UserInfo] AS[u]
                            LEFT JOIN UserInfoModuleElement AS ue ON[u].[ID] = ue.UserInfoID
                            LEFT JOIN[RoleUserInfo] AS[ru] ON[u].[ID] = [ru].[UserInfoID]
                            LEFT JOIN RoleModuleElement AS re ON[ru].[RoleID] = re.[RoleID]
                            RIGHT JOIN ModuleElement AS e ON re.ModuleElementID = e.ID OR ue.ModuleElementID = e.ID
                            WHERE[u].[ID] = {0}", userID)
                .Select(e => e.ID)
                .ToList();
        }
    }
}
