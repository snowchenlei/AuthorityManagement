

using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.IRepository;

namespace Cl.AuthorityManagement.Services
{
	public partial class ModuleServices: BaseServices<Module>, IModuleServices
    {
		public ModuleServices(IBaseRepository<Module> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class ModuleElementServices: BaseServices<ModuleElement>, IModuleElementServices
    {
		public ModuleElementServices(IBaseRepository<ModuleElement> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class RoleServices: BaseServices<Role>, IRoleServices
    {
		public RoleServices(IBaseRepository<Role> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class UserInfoServices: BaseServices<UserInfo>, IUserInfoServices
    {
		public UserInfoServices(IBaseRepository<UserInfo> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class UserInfoModuleElementServices: BaseServices<UserInfoModuleElement>, IUserInfoModuleElementServices
    {
		public UserInfoModuleElementServices(IBaseRepository<UserInfoModuleElement> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class RoleModuleElementServices: BaseServices<RoleModuleElement>, IRoleModuleElementServices
    {
		public RoleModuleElementServices(IBaseRepository<RoleModuleElement> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class RequestInfoServices: BaseServices<RequestInfo>, IRequestInfoServices
    {
		public RequestInfoServices(IBaseRepository<RequestInfo> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class MonitorInfoServices: BaseServices<MonitorInfo>, IMonitorInfoServices
    {
		public MonitorInfoServices(IBaseRepository<MonitorInfo> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class ErrorInfoServices: BaseServices<ErrorInfo>, IErrorInfoServices
    {
		public ErrorInfoServices(IBaseRepository<ErrorInfo> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class LoginInfoServices: BaseServices<LoginInfo>, ILoginInfoServices
    {
		public LoginInfoServices(IBaseRepository<LoginInfo> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class OperateInfoServices: BaseServices<OperateInfo>, IOperateInfoServices
    {
		public OperateInfoServices(IBaseRepository<OperateInfo> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class AreaServices: BaseServices<Area>, IAreaServices
    {
		public AreaServices(IBaseRepository<Area> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class IPInfoServices: BaseServices<IPInfo>, IIPInfoServices
    {
		public IPInfoServices(IBaseRepository<IPInfo> baseRepository) : base(baseRepository)
        {
        }
	}
	public partial class PushServices: BaseServices<Push>, IPushServices
    {
		public PushServices(IBaseRepository<Push> baseRepository) : base(baseRepository)
        {
        }
	}
}