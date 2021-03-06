﻿using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Cl.AuthorityManagement.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Services
{
    public partial class ModuleElementServices : BaseServices<ModuleElement>, IModuleElementServices
    {
        private readonly IUserInfoModuleElementRepository UserInfoModuleElementRepository = null;
        private readonly IRoleModuleElementRepository RoleModuleElementRepository = null;
        private readonly IModuleRepository ModuleRepository = null;
        public ModuleElementServices(
            IBaseRepository<ModuleElement> baseRepository,
            IUserInfoModuleElementRepository userInfoModuleElementRepository,
            IRoleModuleElementRepository roleModuleElementRepository,
            IModuleRepository moduleRepository) : base(baseRepository)
        {
            UserInfoModuleElementRepository = userInfoModuleElementRepository;
            RoleModuleElementRepository = roleModuleElementRepository;
            ModuleRepository = moduleRepository;
        }


        /// <summary>
        /// 加载选中的元素
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moduleId">模块Id</param>
        /// <param name="roleIds">用户拥有的角色Id</param>
        /// <returns>选中的元素</returns>
        public List<ModuleElement> LoadSelectElements(int userId, int moduleId, int[] roleIds)
        {
            if (roleIds.Contains(1))
            {
                return ModuleRepository
                    .LoadFirst(m => m.Id == moduleId)
                    .ModuleElements
                    .OrderBy(e => e.Sort).ToList();
            }

            List<ModuleElement> elements = new List<ModuleElement>();
            UserInfoModuleElement[] UserInfoModuleElements = UserInfoModuleElementRepository
                .LoadEntities(e => e.UserInfo.Id == userId
                    && e.Module.Id == moduleId)
                .ToArray();
            RoleModuleElement[] roleModuleElements = RoleModuleElementRepository
                .LoadEntities(e => roleIds.Contains(e.Role.Id)
                    && e.Module.Id == moduleId)
                .ToArray();

            foreach (UserInfoModuleElement element in UserInfoModuleElements)
            {
                elements.Add(element.ModuleElement);
            }
            foreach (RoleModuleElement element in roleModuleElements)
            {
                elements.Add(element.ModuleElement);
            }
            elements.Sort();
            return elements;
        }

        /// <summary>
        /// 是否拥有访问模块元素的权限
        /// </summary>
        /// <param name="actionName">控制器名称</param>
        /// <param name="user">登陆用户信息</param>
        /// <returns>是否拥有</returns>
        public bool IsHaveModuleElement(string controllerName, string actionName, UserInfo user)
        {
            int moduleId = ModuleRepository.GetId(controllerName);
            Func<BaseModuleElement, bool> predicate = u => u.Module.Id == moduleId
                    && u.ModuleElement.Action != null
                    && u.ModuleElement.Action.Equals(actionName, StringComparison.InvariantCultureIgnoreCase);

            //用户模块元素线
            bool IsHaveModuleElement = user.UserInfoModuleElements
                .Any(predicate);
            if (IsHaveModuleElement)
            {
                return true;
            }

            //角色模块元素线
            IsHaveModuleElement = user.Roles
                .Any(u => u.RoleModuleElements
                    .Any(predicate));
            if (IsHaveModuleElement)
            {
                return true;
            }
            return false;
        }
    }
}
