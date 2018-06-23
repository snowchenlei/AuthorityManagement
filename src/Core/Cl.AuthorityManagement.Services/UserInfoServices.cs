using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
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
        private readonly IRoleUserInfoRepository RoleUserInfoRepository = null;
        private readonly IRoleModuleRepository RoleModuleRepository = null;
        private readonly IRoleRepository RoleRepository = null;
        private readonly IModuleRepository ModuleRepository = null;
        private readonly IModuleElementRepository ModuleElementRepository = null;
        private readonly IModuleUserInfoRepository ModuleUserInfoRepository = null;
        private readonly IModuleElementModuleRepository ModuleElementModuleRepository = null;
        private readonly IUserInfoModuleElementRepository UserInfoModuleElementRepository = null;
        public UserInfoServices(
            AuthorityManagementContext context,
            IBaseRepository<UserInfo> baseRepository,
            IUserInfoRepository userInfoRepository,
            IRoleRepository roleRepository,
            IRoleModuleRepository roleModuleRepository,
            IModuleRepository moduleRepository,
            IRoleUserInfoRepository roleUserInfoRepository,
            IModuleElementRepository moduleElementRepository,
            IModuleUserInfoRepository moduleUserInfoRepository,
            IModuleElementModuleRepository moduleElementModuleRepository,
            IUserInfoModuleElementRepository userInfoModuleElementRepository) : base(context, baseRepository)
        {
            UserInfoRepository = userInfoRepository;
            RoleRepository = roleRepository;
            ModuleRepository = moduleRepository;
            RoleModuleRepository = roleModuleRepository;
            RoleUserInfoRepository = roleUserInfoRepository;
            ModuleElementRepository = moduleElementRepository;
            ModuleUserInfoRepository = moduleUserInfoRepository;
            ModuleElementModuleRepository = moduleElementModuleRepository;
            UserInfoModuleElementRepository = userInfoModuleElementRepository;
        }

        /// <summary>
        /// 加载用户、角色、模块元素信息
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns>用户信息</returns>
        public async Task<List<UserInfo>> LoadUserRoleModuleElementAsync(Expression<Func<UserInfo, bool>> whereLamada)
        {
            return await UserInfoRepository.LoadUserRoleModuleElementAsync(whereLamada);
        }
        
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public CheckReturn LoadRoles(int userID)
        {
            if (!UserInfoRepository.IsExists(u => u.ID == userID))
            {
                return new CheckReturn
                {
                    Message = "用户不存在",
                    Flag = false
                };
            }
            var roles = RoleRepository.LoadEntities(c => true)
                   .Select(r => new
                   {
                       Id = r.ID,
                       Name = r.Name
                   }).ToDictionary(key => key.Id, value => value.Name);
            int[] ids = RoleUserInfoRepository.LoadRoleIDs(userID);
            return new CheckReturn
            {
                Flag = true,
                Message = "获取成功",
                dics = roles,
                IDs = ids
            };
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public CheckReturn LoadModules(int userID)
        {
            if (!UserInfoRepository.IsExists(u=>u.ID == userID))
            {
                return new CheckReturn
                {
                    Message = "用户不存在",
                    Flag = false
                };
            }
            var modules = ModuleRepository.LoadEntities(c => true, true)
                   .Select(r => new
                   {
                       ID = r.ID,
                       Name = r.Name
                   }).ToDictionary(key => key.ID, value => value.Name);
            int[] ids = ModuleUserInfoRepository.LoadModuleIDs(userID);
            int[] roleIDs = RoleUserInfoRepository
                .LoadEntities(r => r.UserInfoID == userID)
                .Select(r => r.RoleID)
                .ToArray();
            ids = RoleModuleRepository
                .LoadEntities(r => roleIDs.Contains(r.RoleID))
                .Select(r => r.ModuleID)
                .Union(ids).ToArray();
            return new CheckReturn
            {
                Flag = true,
                Message = "获取成功",
                dics = modules,
                IDs = ids
            };
        }
        
        /// <summary>
        /// 获取模块元素
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="moduleID">模块ID</param>
        /// <returns>模块元素</returns>
        public CheckReturn LoadModuleElements(int userID, int moduleID)
        {
            if (!UserInfoRepository.IsExists(u => u.ID == userID))
            {
                return new CheckReturn
                {
                    Message = "用户不存在",
                    Flag = false
                };
            }

            //int[] ids = UserInfoModuleElementRepository
            //    .LoadEntities(e => e.Module.ID == moduleID
            //        && e.UserInfoID == userID)
            //    .Select(e => e.ModuleElementID).ToArray();
            int[] ids = UserInfoRepository
                .LoadUserElementID(userID).ToArray();
            
            var elements = ModuleElementRepository
                .LoadModuleElement(moduleID)
                .Select(e => new
                {
                    Id = e.ID,
                    Name = e.Name
                }).ToDictionary(key => key.Id, value => value.Name);
            return new CheckReturn
            {
                Flag = true,
                Message = "获取成功",
                dics = elements,
                IDs = ids
            };
        }

        /// <summary>
        /// 获取用户模块
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户模块</returns>
        public List<Module> LoadUserModule(int userID)
        {
            return UserInfoRepository
                .LoadUserModule(userID);
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="roleIDs">选中角色Id集合</param>
        /// <returns>是否成功</returns>
        public ReturnDescription SetUserRole(int userID,int[] roleIDs)
        {
            if (!UserInfoRepository.IsExists(u => u.ID == userID))
            {
                return new ReturnDescription
                {
                    Flag = false,
                    Message = "用户不存在"
                };
            }

            RoleUserInfoRepository.RemoveAll(userID);
            foreach (int roleID in roleIDs)
            {
                RoleUserInfoRepository.AddEntity(new RoleUserInfo
                {
                    UserInfoID = userID,
                    RoleID = roleID
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
        /// <param name="userID">用户ID</param>
        /// <param name="moduleIDs">选中模块Id集合</param>
        /// <returns>结果描述</returns>
        public ReturnDescription SetUserModule(int userID, int[] moduleIDs)
        {
            if (!UserInfoRepository.IsExists(u => u.ID == userID))
            {
                return new ReturnDescription
                {
                    Flag = false,
                    Message = "用户不存在"
                };
            }

            ModuleUserInfoRepository.RemoveAll(userID);
            foreach (int moduleID in moduleIDs)
            {
                ModuleUserInfoRepository.AddEntity(new ModuleUserInfo
                {
                    UserInfoID = userID,
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
        /// 设置用户模块元素
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="elementIDs">模块元素ID集合</param>
        /// <param name="moduleID">模块ID</param>
        /// <returns>结果描述</returns>
        public ReturnDescription SetUserModuleElements(int userID, int[] elementIDs, int moduleID)
        {
            if (!UserInfoRepository.IsExists(u => u.ID == userID))
            {
                return new ReturnDescription
                {
                    Flag = false,
                    Message = "用户不存在"
                };
            }

            UserInfoModuleElementRepository.RemoveAll(userID, moduleID);
            foreach (int elementID in elementIDs)
            {
                UserInfoModuleElementRepository.AddEntity(new UserInfoModuleElement
                {
                    UserInfoID = userID,
                    ModuleID = moduleID,
                    ModuleElementID = elementID,
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
        /// 删除用户
        /// </summary>
        /// <param name="userID">要删除的用户编号</param>
        /// <returns>是否成功</returns>
        public bool Delete(int userID)
        {
            var userElements = UserInfoModuleElementRepository
                .LoadEntities(s => s.UserInfo.ID == userID)
                .ToArray();
            foreach (var item in userElements)
            {
                UserInfoModuleElementRepository.DeleteEntity(item);
            }
            UserInfo user = UserInfoRepository.LoadFirst(u => u.ID == userID);
            CurrentRepository.DeleteEntity(user);
            return CurrentContext.SaveChanges()>0;
        }
    }
}
