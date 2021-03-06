﻿using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IServices
{
    public partial interface IModuleElementServices:IBaseServices<ModuleElement>
    {
        List<ModuleElement> LoadModuleElement(int moduleID);

        /// <summary>
        /// 加载模块元素
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="moduleID">模块编号</param>
        /// <returns>用户所拥有的模块元素</returns>
        List<ModuleElement> LoadModuleElement(int userID, int moduleID);

        /// <summary>
        /// 加载选中的元素
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moduleId">模块Id</param>
        /// <param name="roleIds">用户拥有的角色Id</param>
        /// <returns>选中的元素</returns>
        List<ModuleElement> LoadSelectElements(int userId, int moduleId, int[] roleIds);

        /// <summary>
        /// 是否拥有访问模块元素的权限
        /// </summary>
        /// <param name="actionName">控制器名称</param>
        /// <param name="user">登陆用户信息</param>
        /// <returns>是否拥有</returns>
        bool IsHaveModuleElement(string controllerName, string actionName, UserInfo user);
    }
}
