using Cl.AuthorityManagement.Entity;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IModuleUserInfoRepository : IBaseRepository<ModuleUserInfo>
    {
    }
    public partial interface IModuleRepository : IBaseRepository<Module>
    {
    }
    public partial interface IModuleElementRepository : IBaseRepository<ModuleElement>
    {
    }
    public partial interface IRoleRepository : IBaseRepository<Role>
    {
    }
    public partial interface IUserInfoRepository : IBaseRepository<UserInfo>
    {
    }
    public partial interface IUserInfoModuleElementRepository : IBaseRepository<UserInfoModuleElement>
    {
    }
    public partial interface IRoleModuleElementRepository : IBaseRepository<RoleModuleElement>
    {
    }
    public partial interface IRequestInfoRepository : IBaseRepository<RequestInfo>
    {
    }
    public partial interface IMonitorInfoRepository : IBaseRepository<MonitorInfo>
    {
    }
    public partial interface IErrorInfoRepository : IBaseRepository<ErrorInfo>
    {
    }
    public partial interface ILoginInfoRepository : IBaseRepository<LoginInfo>
    {
    }
    public partial interface IOperateInfoRepository : IBaseRepository<OperateInfo>
    {
    }
    public partial interface IAreaRepository : IBaseRepository<Area>
    {
    }
    public partial interface IIPInfoRepository : IBaseRepository<IPInfo>
    {
    }
}