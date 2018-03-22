using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
namespace Cl.AuthorityManagement.Repository
{
    public partial class ModuleRepository:BaseRepository<Module>, IModuleRepository
    {
    }
    public partial class ModuleElementRepository:BaseRepository<ModuleElement>, IModuleElementRepository
    {
    }
    public partial class RoleRepository:BaseRepository<Role>, IRoleRepository
    {
    }
    public partial class PermissionRepository:BaseRepository<Permission>, IPermissionRepository
    {
    }
    public partial class UserInfoRepository:BaseRepository<UserInfo>, IUserInfoRepository
    {
    }
    public partial class UserInfoModuleElementRepository:BaseRepository<UserInfoModuleElement>, IUserInfoModuleElementRepository
    {
    }
    public partial class RoleModuleElementRepository:BaseRepository<RoleModuleElement>, IRoleModuleElementRepository
    {
    }
}
