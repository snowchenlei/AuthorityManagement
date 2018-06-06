using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
namespace Cl.AuthorityManagement.Repository
{
    public partial class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
    }
    public partial class ModuleElementRepository : BaseRepository<ModuleElement>, IModuleElementRepository
    {
    }
    public partial class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
    }
    public partial class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
    }
    public partial class UserInfoModuleElementRepository : BaseRepository<UserInfoModuleElement>, IUserInfoModuleElementRepository
    {
    }
    public partial class RoleModuleElementRepository : BaseRepository<RoleModuleElement>, IRoleModuleElementRepository
    {
    }
    public partial class RequestInfoRepository : BaseRepository<RequestInfo>, IRequestInfoRepository
    {
    }
    public partial class MonitorInfoRepository : BaseRepository<MonitorInfo>, IMonitorInfoRepository
    {
    }
    public partial class ErrorInfoRepository : BaseRepository<ErrorInfo>, IErrorInfoRepository
    {
    }
    public partial class LoginInfoRepository : BaseRepository<LoginInfo>, ILoginInfoRepository
    {
    }
    public partial class OperateInfoRepository : BaseRepository<OperateInfo>, IOperateInfoRepository
    {
    }
    public partial class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
    }
    public partial class IPInfoRepository : BaseRepository<IPInfo>, IIPInfoRepository
    {
    }
}
