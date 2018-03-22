

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
	public partial class PermissionServices: BaseServices<Permission>, IPermissionServices
    {
		public PermissionServices(IBaseRepository<Permission> baseRepository) : base(baseRepository)
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
}