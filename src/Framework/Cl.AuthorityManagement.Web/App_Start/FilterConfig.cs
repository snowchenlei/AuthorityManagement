using Cl.AuthorityManagement.Library.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //自定义异常过滤器
            filters.Add(new CustomerExceptionAttribute());
            filters.Add(new CustomerActionAttribute());
            filters.Add(new CustomerResultAttribute());
            //filters.Add(new CustomerAuthorizeAttribute());
        }
    }
}