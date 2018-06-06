using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using System.Linq;

namespace Cl.AuthorityManagement.Repository
{
    public partial class ModuleRepository : BaseRepository<Module>,IModuleRepository
    {
        /// <summary>
        /// 根据控制器名称获取模块ID
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <returns>模块ID</returns>
        public int GetId(string controllerName)
        {
            return base.LoadEntities(m => m.Url.IndexOf("/" + controllerName + "/") == 0)
                 .Select(m => m.Id)
                 .FirstOrDefault();
        }
    }
}
