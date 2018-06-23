using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Cl.AuthorityManagement.Repository
{
    public partial class ModuleElementModuleRepository:BaseRepository<ModuleElementModule>, IModuleElementModuleRepository
    {
        /// <summary>
        /// 加载元素ID
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>用户的模块ID</returns>
        public int[] LoadElementIDs(int moduleID)
        {
            return CurrentContext.ModuleElementModule
                .AsNoTracking()
                .Where(w => w.ModuleID == moduleID)
                .Select(s => s.ModuleElementID)
                .ToArray();
        }

        /// <summary>
        /// 删除模块元素的模块
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        public void RemoveAll(int moduleID)
        {
            var moduleElementModules = CurrentContext.ModuleElementModule
                .Where(m => m.ModuleID == moduleID);

            CurrentContext.ModuleElementModule
                .RemoveRange(moduleElementModules);
        }
    }
}
