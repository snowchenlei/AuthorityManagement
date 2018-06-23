using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Services
{
    public partial class RoleServices:BaseServices<Role>,IRoleServices
    {
        private readonly IModuleRepository ModuleRepository = null;
        private readonly IRoleModuleRepository RoleModuleRepository = null;
        private readonly IRoleModuleElementRepository RoleModuleElementRepository = null;
        private readonly IModuleElementRepository ModuleElementRepository = null;
        public RoleServices(
            AuthorityManagementContext context,
            IBaseRepository<Role> baseRepository,
            IModuleRepository moduleServices,
            IRoleModuleRepository roleModuleRepository,
            IRoleModuleElementRepository roleModuleElementRepository,
            IModuleElementRepository moduleElementRepository) : base(context, baseRepository)
        {
            ModuleRepository = moduleServices;
            RoleModuleRepository = roleModuleRepository;
            RoleModuleElementRepository = roleModuleElementRepository;
            ModuleElementRepository = moduleElementRepository;
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色模块列表</returns>
        public CheckReturn LoadModules(int roleID)
        {
            if (!CurrentRepository.IsExists(u => u.ID == roleID))
            {
                return new CheckReturn
                {
                    Message = "角色不存在",
                    Flag = false
                };
            }
            var modules = ModuleRepository.LoadEntities(c => true, true)
                   .Select(r => new
                   {
                       ID = r.ID,
                       Name = r.Name
                   }).ToDictionary(key => key.ID, value => value.Name);
            int[] ids = RoleModuleRepository.LoadEntities(r => r.RoleID == roleID)
                .Select(r => r.ModuleID)
                .ToArray();
            return new CheckReturn
            {
                Flag = true,
                Message = "获取成功",
                dics = modules,
                IDs = ids
            };
        }

        /// <summary>
        /// 加载角色模块
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色模块</returns>
        public List<Module> LoadRoleModule(int roleID)
        {
            return ModuleRepository
                .LoadRoleModule(roleID);
        }

        /// <summary>
        /// 获取模块元素列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="moduleID">模块ID</param>
        /// <returns>角色模块元素列表</returns>
        public CheckReturn LoadModuleElements(int roleID, int moduleID)
        {
            if (!CurrentRepository.IsExists(u => u.ID == roleID))
            {
                return new CheckReturn
                {
                    Message = "角色不存在",
                    Flag = false
                };
            }
            var modules = ModuleElementRepository.LoadModuleElement(moduleID)
                   .Select(r => new
                   {
                       ID = r.ID,
                       Name = r.Name
                   }).ToDictionary(key => key.ID, value => value.Name);
            int[] ids = RoleModuleElementRepository.LoadEntities(r => r.RoleID == roleID && r.ModuleID == moduleID)
                .Select(r => r.ModuleElementID)
                .ToArray();
            return new CheckReturn
            {
                Flag = true,
                Message = "获取成功",
                dics = modules,
                IDs = ids
            };
        }

        /// <summary>
        /// 设置用户模块
        /// </summary>
        /// <param name="roleID">用户ID</param>
        /// <param name="moduleIDs">选中模块Id集合</param>
        /// <returns>结果描述</returns>
        public ReturnDescription SetRoleModule(int roleID, int[] moduleIDs)
        {
            if (!CurrentRepository.IsExists(u => u.ID == roleID))
            {
                return new ReturnDescription
                {
                    Flag = false,
                    Message = "角色不存在"
                };
            }
            RoleModuleRepository.RemoveAll(roleID);
            CurrentContext.SaveChanges();
            foreach (int moduleID in moduleIDs)
            {
                RoleModuleRepository.AddEntity(new RoleModule
                {
                    RoleID = roleID,
                    ModuleID = moduleID
                });
            }
            if (CurrentContext.SaveChanges() > 0)
            {
                return new ReturnDescription
                {
                    Flag = true,
                    Message = "设置成功"
                };
            }
            else
            {
                return new ReturnDescription
                {
                    Flag = false,
                    Message = "设置失败"
                };
            }
        }

        /// <summary>
        /// 设置用户模块
        /// </summary>
        /// <param name="roleID">用户ID</param>
        /// <param name="moduleElementIDs">选中模块Id集合</param>
        /// <returns>结果描述</returns>
        public ReturnDescription SetRoleModuleElements(int roleID, int[] moduleElementIDs, int moduleID)
        {
            if (!CurrentRepository.IsExists(u => u.ID == roleID))
            {
                return new ReturnDescription
                {
                    Flag = false,
                    Message = "角色不存在"
                };
            }

            RoleModuleElementRepository.RemoveAll(roleID, moduleID);
            foreach (int elementID in moduleElementIDs)
            {
                RoleModuleElementRepository.AddEntity(new RoleModuleElement
                {
                    RoleID = roleID,
                    ModuleID = moduleID,
                    ModuleElementID = elementID
                });
            }
            if (CurrentContext.SaveChanges() > 0)
            {
                return new ReturnDescription
                {
                    Flag = true,
                    Message = "设置成功"
                };
            }
            else
            {
                return new ReturnDescription
                {
                    Flag = false,
                    Message = "设置失败"
                };
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">要删除的角色编号</param>
        /// <returns>是否成功</returns>
        public bool Delete(int roleId)
        {
            var userElements = RoleModuleElementRepository.LoadEntities(s => s.Role.ID == roleId);
            foreach (var item in userElements)
            {
                RoleModuleElementRepository.DeleteEntity(item);
            }
            CurrentContext.SaveChanges();

            Role role = CurrentRepository.LoadFirst(u => u.ID == roleId);
            CurrentRepository.DeleteEntity(role);
            return CurrentContext.SaveChanges()>0;
        }
    }
}
