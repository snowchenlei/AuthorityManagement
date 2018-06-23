using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cl.AuthorityManagement.Repository
{
    public partial class ModuleUserInfoRepository:BaseRepository<ModuleUserInfo>, IModuleUserInfoRepository
    {
        public List<Module> LoadModule(int userID)
        {
            return CurrentContext
                .ModuleUserInfo
                .AsNoTracking()
                .Where(m => m.UserInfoID == userID)
                .Select(m => m.Module)
                .ToList();
        }

        /// <summary>
        /// 加载模块ID
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户的模块ID</returns>
        public int[] LoadModuleIDs(int userID)
        {
            return CurrentContext.ModuleUserInfo
                .AsNoTracking()
                .Where(w => w.UserInfoID == userID)
                .Select(s => s.ModuleID)
                .ToArray();
        }

        /// <summary>
        /// 删除用户的模块
        /// </summary>
        /// <param name="userID">用户ID</param>
        public void RemoveAll(int userID)
        {
            var moduleUsers = CurrentContext.ModuleUserInfo
                .Where(m => m.UserInfoID == userID);

            CurrentContext.ModuleUserInfo
                .RemoveRange(moduleUsers);
        }
    }
}
