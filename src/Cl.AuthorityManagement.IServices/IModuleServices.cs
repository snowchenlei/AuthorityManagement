using Cl.AuthorityManagement.Entity;
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
        /// 设置模块元素
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="elementIds">元素Id</param>
        /// <returns></returns>
        bool SetModuleElements(Module module, int[] elementIds);


        /// <summary>
        /// 加载选中的模块
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>选中的模块</returns>
        List<Module> LoadSelectModules(UserInfo userInfo);

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
