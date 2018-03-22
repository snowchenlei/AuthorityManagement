using Cl.AuthorityManagement.Entity;

namespace Cl.AuthorityManagement.IServices
{
	public partial interface IModuleServices:IBaseServices<Module>
	{
	}
	public partial interface IModuleElementServices:IBaseServices<ModuleElement>
	{
	}
	public partial interface IRoleServices:IBaseServices<Role>
	{
	}
	public partial interface IPermissionServices:IBaseServices<Permission>
	{
	}
	public partial interface IUserInfoServices:IBaseServices<UserInfo>
	{
	}
	public partial interface IUserInfoModuleElementServices:IBaseServices<UserInfoModuleElement>
	{
	}
	public partial interface IRoleModuleElementServices:IBaseServices<RoleModuleElement>
	{
	}
}