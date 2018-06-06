

using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.IRepository;
using Cl.AuthorityManagement.Data;

namespace Cl.AuthorityManagement.Services
{
    public partial class ModuleServices : BaseServices<Module>, IModuleServices
    {
        public ModuleServices(AuthorityManagementContext context, IBaseRepository<Module> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class ModuleElementServices : BaseServices<ModuleElement>, IModuleElementServices
    {
        public ModuleElementServices(AuthorityManagementContext context, IBaseRepository<ModuleElement> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class RoleServices : BaseServices<Role>, IRoleServices
    {
        public RoleServices(AuthorityManagementContext context, IBaseRepository<Role> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class UserInfoServices : BaseServices<UserInfo>, IUserInfoServices
    {
        public UserInfoServices(AuthorityManagementContext context, IBaseRepository<UserInfo> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class UserInfoModuleElementServices : BaseServices<UserInfoModuleElement>, IUserInfoModuleElementServices
    {
        public UserInfoModuleElementServices(AuthorityManagementContext context, IBaseRepository<UserInfoModuleElement> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class RoleModuleElementServices : BaseServices<RoleModuleElement>, IRoleModuleElementServices
    {
        public RoleModuleElementServices(AuthorityManagementContext context, IBaseRepository<RoleModuleElement> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class RequestInfoServices : BaseServices<RequestInfo>, IRequestInfoServices
    {
        public RequestInfoServices(AuthorityManagementContext context, IBaseRepository<RequestInfo> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class MonitorInfoServices : BaseServices<MonitorInfo>, IMonitorInfoServices
    {
        public MonitorInfoServices(AuthorityManagementContext context, IBaseRepository<MonitorInfo> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class ErrorInfoServices : BaseServices<ErrorInfo>, IErrorInfoServices
    {
        public ErrorInfoServices(AuthorityManagementContext context, IBaseRepository<ErrorInfo> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class LoginInfoServices : BaseServices<LoginInfo>, ILoginInfoServices
    {
        public LoginInfoServices(AuthorityManagementContext context, IBaseRepository<LoginInfo> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class OperateInfoServices : BaseServices<OperateInfo>, IOperateInfoServices
    {
        public OperateInfoServices(AuthorityManagementContext context, IBaseRepository<OperateInfo> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class AreaServices : BaseServices<Area>, IAreaServices
    {
        public AreaServices(AuthorityManagementContext context, IBaseRepository<Area> baseRepository) : base(context, baseRepository)
        {
        }
    }
    public partial class IPInfoServices : BaseServices<IPInfo>, IIPInfoServices
    {
        public IPInfoServices(AuthorityManagementContext context, IBaseRepository<IPInfo> baseRepository) : base(context, baseRepository)
        {
        }
    }
}