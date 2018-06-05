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
    public partial class RoleServices:BaseServices<Role>,IRoleServices
    {
        public readonly IModuleRepository ModuleRepository = null;
        private readonly IRoleModuleElementRepository RoleModuleElementRepository = null;
        private readonly IModuleElementRepository ModuleElementRepository = null;
        public RoleServices(
            IBaseRepository<Role> baseRepository,
            IModuleRepository moduleServices,
            IRoleModuleElementRepository roleModuleElementRepository,
            IModuleElementRepository moduleElementRepository) : base(baseRepository)
        {
            ModuleRepository = moduleServices;
            RoleModuleElementRepository = roleModuleElementRepository;
            ModuleElementRepository = moduleElementRepository;
        }

        public bool SetRoleModule(Role role, int[] moduleIds)
        {
            if (role == null)
            {
                throw new ArgumentNullException("角色不能为空");
            }
            role.Modules.Clear();
            Module[] modules = ModuleRepository
                .LoadEntities(r => moduleIds.Contains(r.Id))
                .ToArray();
            foreach (Module module in modules)
            {
                role.Modules.Add(module);
            }
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 设置角色模块元素
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="elementIds">元素id集合</param>
        /// <param name="moduleId">模块id</param>
        /// <returns>是否成功</returns>
        public bool SetRoleModuleElements(Role role, int[] elementIds, int moduleId)
        {
            if (role == null)
            {
                throw new ArgumentNullException("角色不能为空");
            }
            //非多对多不可用clear，需要手动删除
            RoleModuleElement[] roleElements = role.RoleModuleElements.ToArray();
            foreach (RoleModuleElement item in roleElements)
            {
                RoleModuleElementRepository.DeleteEntity(item);
            }
            Module module = ModuleRepository
                .LoadFirst(m => m.Id == moduleId);
            ModuleElement[] elements = ModuleElementRepository
                .LoadEntities(m => elementIds.Contains(m.Id))
                .ToArray();
            foreach (ModuleElement element in elements)
            {
                role.RoleModuleElements.Add(new RoleModuleElement
                {
                    Module = module,
                    ModuleElement = element,
                    Role = role
                });
            }
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">要删除的角色编号</param>
        /// <returns>是否成功</returns>
        public bool Delete(int roleId)
        {
            var userElements = RoleModuleElementRepository.LoadEntities(s => s.Role.Id == roleId);
            foreach (var item in userElements)
            {
                RoleModuleElementRepository.DeleteEntity(item);
            }
            CurrentDBSession.SaveChanges();

            Role role = CurrentRepository.LoadFirst(u => u.Id == roleId);
            CurrentRepository.DeleteEntity(role);
            return CurrentDBSession.SaveChanges();
        }
    }
}
