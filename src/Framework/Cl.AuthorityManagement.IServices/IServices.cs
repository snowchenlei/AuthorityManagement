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
	public partial interface IUserInfoServices:IBaseServices<UserInfo>
	{
	}
	public partial interface IUserInfoModuleElementServices:IBaseServices<UserInfoModuleElement>
	{
	}
	public partial interface IRoleModuleElementServices:IBaseServices<RoleModuleElement>
	{
	}
	public partial interface IRequestInfoServices:IBaseServices<RequestInfo>
	{
	}
	public partial interface IMonitorInfoServices:IBaseServices<MonitorInfo>
	{
	}
	public partial interface IErrorInfoServices:IBaseServices<ErrorInfo>
	{
	}
	public partial interface ILoginInfoServices:IBaseServices<LoginInfo>
	{
	}
	public partial interface IOperateInfoServices:IBaseServices<OperateInfo>
	{
	}
	public partial interface IAreaServices:IBaseServices<Area>
	{
	}
	public partial interface IIPInfoServices:IBaseServices<IPInfo>
	{
	}
}