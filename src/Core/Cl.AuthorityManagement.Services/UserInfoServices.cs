using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Cl.AuthorityManagement.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            AuthorityManagementContext context,
            IBaseRepository<UserInfo> baseRepository,
            IUserInfoRepository userInfoRepository,
            IRoleRepository roleRepository,
            IModuleRepository moduleRepository,
            IModuleElementRepository moduleElementRepository,
            IUserInfoModuleElementRepository userInfoModuleElementRepository) : base(context, baseRepository)
        {
            UserInfoRepository = userInfoRepository;
            RoleRepository = roleRepository;
            ModuleRepository = moduleRepository;
            ModuleElementRepository = moduleElementRepository;
            UserInfoModuleElementRepository = userInfoModuleElementRepository;
        }
        public async Task<UserInfo> LoadUserInfoAsync(Expression<Func<UserInfo, bool>> whereLamada)
        {
            return await UserInfoRepository.LoadUserInfoAsync(whereLamada);
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
            user.RoleUserInfos.Clear();
            Role[] roles = RoleRepository
                .LoadEntities(r => roleIds.Contains(r.ID))
                .ToArray();
            foreach (Role role in roles)
            {
                user.RoleUserInfos.Add(new RoleUserInfo
                {
                    UserInfo = user,
                    Role = role
                });
            }
            return CurrentContext.SaveChanges()>0;
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
            user.ModuleUserInfos.Clear();
            Module[] modules = ModuleRepository
                .LoadEntities(m => moduleIds.Contains(m.ID))
                .ToArray();
            foreach (Module module in modules)
            {
                user.ModuleUserInfos.Add(new ModuleUserInfo
                {
                    Module = module,
                    UserInfo = user
                });
            }
            return CurrentContext.SaveChanges()>0;
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
                .LoadFirst(m => m.ID == moduleId);
            ModuleElement[] elements = ModuleElementRepository
                .LoadEntities(m => elementIds.Contains(m.ID))
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
            return CurrentContext.SaveChanges()>0;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">要删除的用户编号</param>
        /// <returns>是否成功</returns>
        public bool Delete(int userId)
        {
            var userElements = UserInfoModuleElementRepository
                .LoadEntities(s => s.UserInfo.ID == userId)
                .ToArray();
            foreach (var item in userElements)
            {
                UserInfoModuleElementRepository.DeleteEntity(item);
            }
            UserInfo user = UserInfoRepository.LoadFirst(u => u.ID == userId);
            CurrentRepository.DeleteEntity(user);
            return CurrentContext.SaveChanges()>0;
        }
    }
}
