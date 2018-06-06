using Cl.AuthorityManagement.Data;
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
        private readonly IModuleRepository ModuleRepository = null;
        private readonly IModuleElementRepository ModuleElementRepository = null;
        private readonly IRoleUserInfoRepository RoleUserInfoRepository = null;
        private readonly IModuleUserInfoRepository ModuleUserInfoRepository = null;
        private readonly IRoleModuleElementRepository RoleModuleElementRepository = null;
        private readonly IUserInfoModuleElementRepository UserInfoModuleElementRepository = null;
        public ModuleServices(
            AuthorityManagementContext context,
            IBaseRepository<Module> baseRepository,
            IModuleRepository moduleRepository,
            IModuleElementRepository moduleElementRepository,
            IModuleUserInfoRepository moduleUserInfoRepository,
            IRoleUserInfoRepository roleUserInfoRepository,
            IRoleModuleElementRepository roleModuleElementRepository,
            IUserInfoModuleElementRepository userInfoModuleElementRepository) :base(context, baseRepository)
        {
            ModuleRepository = moduleRepository;
            ModuleElementRepository = moduleElementRepository;
            RoleUserInfoRepository = roleUserInfoRepository;
            ModuleUserInfoRepository = moduleUserInfoRepository;
            RoleModuleElementRepository = roleModuleElementRepository;
            UserInfoModuleElementRepository = userInfoModuleElementRepository;
        }
        
        /// <summary>
        /// 加载选中的模块（用户和角色）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>选中的模块</returns>
        public List<Module> LoadSelectModules(UserInfo userInfo)
        {
            List<RoleUserInfo> roleUserInfos = RoleUserInfoRepository
                .LoadRoleUserInfo(r => r.UserInfoID == userInfo.ID);
            if (roleUserInfos.Any(r=>r.RoleID == 1))
            {
                return CurrentRepository.LoadEntities(m => true).ToList();
            }
            List<Module> modules = ModuleUserInfoRepository
                .LoadModule(userInfo.ID);
            foreach (Role role in roleUserInfos.Select(r=>r.Role))
            {
                modules.AddRange(role.RoleModules.Select(r => r.Module));
            }
            modules.Sort();
            return modules;
        }

        /// <summary>
        /// 设置模块元素
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="elementIDs">元素Id</param>
        /// <returns></returns>
        public bool SetModuleElements(int moduleID, int[] elementIDs)
        {
            Module module = ModuleRepository
                //.LoadModuleElementModules(moduleID);
                .LoadFirst(m => m.ID == moduleID);

            if (module == null)
            {
                throw new ArgumentNullException("模块不能为空");
            }
            module.ModuleElementModules.Clear();
            ModuleElement[] modules = ModuleElementRepository
                .LoadEntities(r => elementIDs.Contains(r.ID))
                .ToArray();
            foreach (ModuleElement element in modules)
            {
                module.ModuleElementModules.Add(new ModuleElementModule
                {
                    Module = module,
                    ModuleElement = element
                });
            }
            return CurrentContext.SaveChanges() > 0;
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
            if (user.ModuleUserInfos.Select(m=>m.Module).Any(predicate))
            {
                return true;
            }
            //用户角色模块线
            bool isHaveModule = user.RoleUserInfos
                .Any(r => r.Role.RoleModules.Select(m=>m.Module)
                    .Any(predicate));

            if (isHaveModule)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除指定模块
        /// </summary>
        /// <param name="module">要删除的模块</param>
        /// <returns>是否成功</returns>
        public bool DelectModule(Module module)
        {
            RoleModuleElement[] roleElements = RoleModuleElementRepository
                .LoadEntities(e => e.Module.ID == module.ID)
                .ToArray();
            UserInfoModuleElement[] userElements = UserInfoModuleElementRepository
                .LoadEntities(e => e.Module.ID == module.ID)
                .ToArray();
            foreach (RoleModuleElement item in roleElements)
            {
                RoleModuleElementRepository.DeleteEntity(item);
            }
            foreach (UserInfoModuleElement item in userElements)
            {
                UserInfoModuleElementRepository.DeleteEntity(item);
            }
            module.ModuleElementModules.Clear();
            module.RoleModules.Clear();
            module.ModuleUserInfos.Clear();
            CurrentRepository.DeleteEntity(module);
            return CurrentContext.SaveChanges() > 0;
        }
    }
}
