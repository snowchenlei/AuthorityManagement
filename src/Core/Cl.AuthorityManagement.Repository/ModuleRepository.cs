using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cl.AuthorityManagement.Repository
{
    public partial class ModuleRepository : BaseRepository<Module>,IModuleRepository
    {
        /// <summary>
        /// 根据模块ID加载模块和元素匹配
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>模块</returns>
        public Module LoadModules(int moduleID)
        {
            return CurrentContext.Module
                .Include(m => m.ModuleElementModules)
                .AsNoTracking()
                .SingleOrDefault(m => m.ID == moduleID);            
        }

        /// <summary>
        /// 根据控制器名称获取模块ID
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <returns>模块ID</returns>
        public int GetId(string controllerName)
        {
            return base.LoadEntities(m => m.Url.IndexOf("/" + controllerName + "/") == 0)
                 .Select(m => m.ID)
                 .FirstOrDefault();
        }

        /// <summary>
        /// 加载角色模块
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色模块</returns>
        public List<Module> LoadRoleModule(int roleID)
        {
            return CurrentContext.RoleModule
                .Include(i => i.Module)
                .AsNoTracking()
                .Where(r => r.RoleID == roleID)
                .Select(r => r.Module).Include(i => i.Parent)
                .ToList();

        }
    }
}
