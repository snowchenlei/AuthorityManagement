using Cl.AuthorityManagement.Entity;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IModuleRepository:IBaseRepository<Module>
    {
    }
    public partial interface IModuleElementRepository:IBaseRepository<ModuleElement>
    {
    }
    public partial interface IRoleRepository:IBaseRepository<Role>
    {
    }
    public partial interface IPermissionRepository:IBaseRepository<Permission>
    {
    }
    public partial interface IUserInfoRepository:IBaseRepository<UserInfo>
    {
    }
    public partial interface IUserInfoModuleElementRepository:IBaseRepository<UserInfoModuleElement>
    {
    }
    public partial interface IRoleModuleElementRepository:IBaseRepository<RoleModuleElement>
    {
    }
}