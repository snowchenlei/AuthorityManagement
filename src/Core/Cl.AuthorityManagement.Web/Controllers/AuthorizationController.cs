using Autofac;
using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Library.Mvc;
using Cl.AuthorityManagement.Model.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class AuthorizationController : BaseController
    {
        private readonly IModuleServices ModuleServices = null;
        private readonly IModuleElementServices ModuleElementServices = null;
        //public AuthorizationController(
        //    IModuleServices moduleServices,
        //    IModuleElementServices moduleElementServices)
        //{
        //    ModuleServices = moduleServices;
        //    ModuleElementServices = moduleElementServices;
        //}

        protected override void Authorization(ref ActionExecutingContext filterContext)
        {
            if (!UserInfo.RoleUserInfos.Any(r => r.RoleID == 1))
            {
                //判断用户是否具有访问方法的权限
                //IModuleServices moduleServices = AutoFacConfig.Container.Resolve<IModuleServices>();

                if (ModuleServices.IsHaveModule(ControllerName, UserInfo))
                {
                    Attribute authorize = Function.GetCustomAttribute(typeof(AuthenticateAttribute));
                    if (authorize != null)
                    {
                        //IModuleElementServices moduleElementServices = AutoFacConfig.Container.Resolve<IModuleElementServices>();
                        if (!ModuleElementServices.IsHaveModuleElement(ControllerName, ActionName, UserInfo))
                        {
                            filterContext.Result = new RedirectResult("~/HttpError/401.html");
                            return;
                            //throw new Exception("你没有权限");
                        }
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/HttpError/401.html");
                }
            }
        }

        protected IOrderedQueryable<T> Sort<T, S>(IQueryable<T> resource, Expression<Func<T, S>> orderbyLamada, OrderType orderType)
        {
            IOrderedQueryable<T> result = null;
            switch (orderType)
            {
                default:
                case OrderType.ASC:
                    result = resource.OrderBy(orderbyLamada);
                    break;
                case OrderType.DESC:
                    result = resource.OrderByDescending(orderbyLamada);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 枚举转下拉
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        protected IList<Select2Model> EnumToSelect(Type enumType)
        {
            string[] fields = System.Enum.GetNames(enumType);
            var results = fields.Select(f => new Select2Model
            {
                id = Convert.ToInt32(System.Enum.Parse(enumType, f)),
                text = EnumHelper.GetAttribute<object, DisplayAttribute>(System.Enum.ToObject(enumType, Convert.ToInt32(System.Enum.Parse(enumType, f))))?.Name
            }).ToList();
            results.Insert(0, new Select2Model { id = -1, text = "请选择" });
            return results;
        }
    }
}