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
    }
}
