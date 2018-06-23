using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IServices
{
    public partial interface IModuleServices : IBaseServices<Module>
    {
        /// <summary>
        /// 加载元素
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>模块元素</returns>
        CheckReturn LoadElements(int moduleID);

        /// <summary>
        /// 加载选中的模块
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>选中的模块</returns>
        List<Module> LoadSelectModules(UserInfo userInfo);

        /// <summary>
        /// 设置模块元素
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="elementIDs">元素Id</param>
        /// <returns></returns>
        ReturnDescription SetModuleElements(int moduleID, int[] elementIDs);

        /// <summary>
        /// 是否拥有访问模块的权限
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="user">登陆用户信息</param>
        /// <returns>是否拥有</returns>
        bool IsHaveModule(string controllerName, UserInfo user);

        /// <summary>
        /// 删除指定模块
        /// </summary>
        /// <param name="module">要删除的模块</param>
        /// <returns>是否成功</returns>
        bool DelectModule(Module module);
    }
}
