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
    public partial class UserInfoServices : BaseServices<UserInfo>, IUserInfoServices
    {
        private readonly IUserInfoRepository UserInfoRepository = null;
        private readonly IRoleRepository RoleRepository = null;
        private readonly IModuleRepository ModuleRepository = null;
        private readonly IModuleElementRepository ModuleElementRepository = null;
        private readonly IUserInfoModuleElementRepository UserInfoModuleElementRepository = null;
        public UserInfoServices(
            IBaseRepository<UserInfo> baseRepository,
            IUserInfoRepository userInfoRepository,
            IRoleRepository roleRepository,
            IModuleRepository moduleRepository,
            IModuleElementRepository moduleElementRepository,
            IUserInfoModuleElementRepository userInfoModuleElementRepository) : base(baseRepository)
        {
            UserInfoRepository = userInfoRepository;
            RoleRepository = roleRepository;
            ModuleRepository = moduleRepository;
            ModuleElementRepository = moduleElementRepository;
            UserInfoModuleElementRepository = userInfoModuleElementRepository;
        }
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="user">要设置的用户</param>
        /// <param name="roleIds">角色Id集合</param>
        /// <returns>是否成功</returns>
        public bool SetUserRole(UserInfo user,int[] roleIds)
        {
            if(user == null)
            {
                throw new ArgumentNullException("用户不能为空");
            }
            user.Roles.Clear();
            Role[] roles = RoleRepository
                .LoadEntities(r => roleIds.Contains(r.Id))
                .ToArray();
            foreach (Role role in roles)
            {
                user.Roles.Add(role);
            }
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 设置用户模块
        /// </summary>
        /// <param name="user">要设置的用户</param>
        /// <param name="roleIds">模块Id集合</param>
        /// <returns>是否成功</returns>
        public bool SetUserModule(UserInfo user,int[] moduleIds)
        {
            if (user == null)
            {
                throw new ArgumentNullException("用户不能为空");
            }
            user.Modules.Clear();
            Module[] modules = ModuleRepository
                .LoadEntities(m => moduleIds.Contains(m.Id))
                .ToArray();
            foreach (Module module in modules)
            {
                user.Modules.Add(module);
            }
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 设置用户模块元素
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="elementIds">元素id集合</param>
        /// <param name="moduleId">模块id</param>
        /// <returns>是否成功</returns>
        public bool SetUserModuleElements(UserInfo user, int[] elementIds, int moduleId)
        {
            if (user == null)
            {
                throw new ArgumentNullException("用户不能为空");
            }
            //非多对多不可用clear，需要手动删除
            UserInfoModuleElement[] userElements = user.UserInfoModuleElements.ToArray();
            foreach (var userInfoModuleElement in userElements)
            {
                UserInfoModuleElementRepository.DeleteEntity(userInfoModuleElement);
            }
            Module module = ModuleRepository
                .LoadFirst(m => m.Id == moduleId);
            ModuleElement[] elements = ModuleElementRepository
                .LoadEntities(m => elementIds.Contains(m.Id))
                .ToArray();
            foreach (ModuleElement element in elements)
            {
                user.UserInfoModuleElements.Add(new UserInfoModuleElement
                {
                    Module = module,
                    ModuleElement = element,
                    UserInfo = user
                });
            }
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">要删除的用户编号</param>
        /// <returns>是否成功</returns>
        public bool Delete(int userId)
        {
            var userElements = UserInfoModuleElementRepository
                .LoadEntities(s => s.UserInfo.Id == userId)
                .ToArray();
            foreach (var item in userElements)
            {
                UserInfoModuleElementRepository.DeleteEntity(item);
            }
            UserInfo user = UserInfoRepository.LoadFirst(u => u.Id == userId);
            CurrentRepository.DeleteEntity(user);
            return CurrentDBSession.SaveChanges();
        }
    }
}
