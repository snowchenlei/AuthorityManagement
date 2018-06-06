using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cl.AuthorityManagement.Repository
{
    public partial class ModuleElementRepository:BaseRepository<ModuleElement>, IModuleElementRepository
    {
        /// <summary>
        /// 获取模块所有元素
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>元素集合</returns>
        public List<ModuleElement> LoadModuleElement(int moduleID)
        {
            return CurrentContext.ModuleElementModule
                .Include(m => m.ModuleElement)
                .AsNoTracking()
                .Where(m => m.ModuleID == moduleID)
                .Select(m => m.ModuleElement)
                .ToList();
        }

        /// <summary>
        /// 获取模块的所有元素ID
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>所有元素ID</returns>
        public List<int> LoadModuleElementID(int moduleID)
        {
            return CurrentContext.ModuleElementModule
                .AsNoTracking()
                .Where(m => m.ModuleID == moduleID)
                .Select(m => m.ModuleElementID)
                .ToList();
        }
    }
}
