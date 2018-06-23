using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cl.AuthorityManagement.Repository
{
    public partial class UserInfoModuleElementRepository:BaseRepository<UserInfoModuleElement>, IUserInfoModuleElementRepository
    {
        /// <summary>
        /// 删除用户的元素
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="moduleID">模块ID</param>
        public void RemoveAll(int userID, int moduleID)
        {
            var userElements = CurrentContext.UserInfoModuleElement
                .Where(m => m.UserInfoID == userID && m.ModuleID == moduleID);

            CurrentContext.UserInfoModuleElement
                .RemoveRange(userElements);
        }
    }
}
