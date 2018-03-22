using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Cl.AuthorityManagement.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Services
{
    public partial class ModuleServices:BaseServices<Module>, IModuleServices
    {
        private readonly IModuleElementRepository ModuleElementRepository = null;
        public ModuleServices(
            IBaseRepository<Module> baseRepository,
            IModuleElementRepository moduleElementRepository):base(baseRepository)
        {
            ModuleElementRepository = moduleElementRepository;
        }

        /// <summary>
        /// 设置模块元素
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="elementIds">元素Id</param>
        /// <returns></returns>
        public bool SetModuleElements(Module module, int[] elementIds)
        {
            if (module == null)
            {
                throw new ArgumentNullException("模块不能为空");
            }
            module.ModuleElements.Clear();
            ModuleElement[] modules = ModuleElementRepository
                .LoadEntities(r => elementIds.Contains(r.Id))
                .ToArray();
            foreach (ModuleElement element in modules)
            {
                module.ModuleElements.Add(element);
            }
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 加载选中的模块（用户和角色）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>选中的模块</returns>
        public List<Module> LoadSelectModules(UserInfo userInfo)
        {
            if(userInfo.Roles.Any(r=>r.Id == 1))
            {
                return CurrentRepository.LoadEntities(m => true).ToList();
            }
            List<Module> modules = new List<Module>();
            modules.AddRange(userInfo.Modules);
            foreach (Role role in userInfo.Roles)
            {
                modules.AddRange(role.Modules);
            }
            modules.Sort();
            return modules;
        }
    }
}
