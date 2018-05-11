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

        /// <summary>
        /// 是否拥有访问模块的权限
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="user">登陆用户信息</param>
        /// <returns>是否拥有</returns>
        public bool IsHaveModule(string controllerName, UserInfo user)
        {
            Func<Module, bool> predicate = m => m.Parent != null && m.Url.IndexOf("/" + controllerName + "/") == 0;
            //用户模块线
            if (user.Modules.Any(predicate))
            {
                return true;
            }
            //用户角色模块线
            bool isHaveModule = user.Roles
                .Any(r => r.Modules
                    .Any(predicate));

            if (isHaveModule)
            {
                return true;
            }
            return false;
        }
    }
}
