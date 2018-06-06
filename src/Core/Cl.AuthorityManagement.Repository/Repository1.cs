using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
namespace Cl.AuthorityManagement.Repository
{
    
         public partial class ModuleUserInfoRepository : BaseRepository<ModuleUserInfo>, IModuleUserInfoRepository
    {
        public ModuleUserInfoRepository(AuthorityManagementContext context) : base(context)
        {
        }
    }
    public partial class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(AuthorityManagementContext context) : base(context)
        {
        }
    }
    public partial class ModuleElementRepository : BaseRepository<ModuleElement>, IModuleElementRepository
    {
        public ModuleElementRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class UserInfoModuleElementRepository : BaseRepository<UserInfoModuleElement>, IUserInfoModuleElementRepository
    {
        public UserInfoModuleElementRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class RoleModuleElementRepository : BaseRepository<RoleModuleElement>, IRoleModuleElementRepository
    {
        public RoleModuleElementRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class RequestInfoRepository : BaseRepository<RequestInfo>, IRequestInfoRepository
    {
        public RequestInfoRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class MonitorInfoRepository : BaseRepository<MonitorInfo>, IMonitorInfoRepository
    {
        public MonitorInfoRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class ErrorInfoRepository : BaseRepository<ErrorInfo>, IErrorInfoRepository
    {
        public ErrorInfoRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class LoginInfoRepository : BaseRepository<LoginInfo>, ILoginInfoRepository
    {
        public LoginInfoRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class OperateInfoRepository : BaseRepository<OperateInfo>, IOperateInfoRepository
    {
        public OperateInfoRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        public AreaRepository(AuthorityManagementContext context) : base(context) { }
    }
    public partial class IPInfoRepository : BaseRepository<IPInfo>, IIPInfoRepository
    {
        public IPInfoRepository(AuthorityManagementContext context) : base(context) { }
    }
}
