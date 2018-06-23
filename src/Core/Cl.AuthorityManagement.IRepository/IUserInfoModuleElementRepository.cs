using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IUserInfoModuleElementRepository:IBaseRepository<UserInfoModuleElement>
    {
        /// <summary>
        /// 删除用户的元素
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="moduleID">模块ID</param>
        void RemoveAll(int userID, int moduleID);
    }
}
