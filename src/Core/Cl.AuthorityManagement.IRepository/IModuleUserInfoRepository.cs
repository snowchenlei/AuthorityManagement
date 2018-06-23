using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IModuleUserInfoRepository:IBaseRepository<ModuleUserInfo>
    {
        List<Module> LoadModule(int userID);

        /// <summary>
        /// 加载模块ID
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户的模块ID</returns>
        int[] LoadModuleIDs(int userID);

        /// <summary>
        /// 删除用户的模块
        /// </summary>
        /// <param name="userID">用户ID</param>
        void RemoveAll(int userID);
    }
}
