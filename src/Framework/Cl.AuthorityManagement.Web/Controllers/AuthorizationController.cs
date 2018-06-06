using Autofac;
using Autofac.Extras.DynamicProxy;
using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Library.Mvc;
using Cl.AuthorityManagement.Model.Mvc;
using Cl.AuthorityManagement.Web.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class AuthorizationController : BaseController
    {
        protected override void Authorization(ref ActionExecutingContext filterContext)
        {
            if (!UserInfo.Roles.Any(r => r.Id == 1))
            {
                //判断用户是否具有访问方法的权限
                IModuleServices moduleServices = AutoFacConfig.Container.Resolve<IModuleServices>();

                if (moduleServices.IsHaveModule(ControllerName, UserInfo))
                {
                    Attribute authorize = Function.GetCustomAttribute(typeof(AuthenticateAttribute));
                    if (authorize != null)
                    {
                        IModuleElementServices moduleElementServices = AutoFacConfig.Container.Resolve<IModuleElementServices>();
                        if (!moduleElementServices.IsHaveModuleElement(ControllerName, ActionName, UserInfo))
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