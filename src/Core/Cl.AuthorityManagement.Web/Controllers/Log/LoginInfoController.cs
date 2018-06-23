using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class LoginInfoController : AuthorizationController
    {
        private readonly ILoginInfoServices LoginInfoServices = null;
        public LoginInfoController(
            ILoginInfoServices loginInfoServices)
        {
            LoginInfoServices = loginInfoServices;
        }

        // GET: Manager/LoginInfo
        [ResponseCache(CacheProfileName = "Header")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Load(int pageIndex, int pageSize, string sort,
           OrderType order, string userName)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);

            var tempLogins = LoginInfoServices.LoadEntities(r => true);
            #region 查询
            if (!String.IsNullOrEmpty(userName))
            {
                tempLogins = tempLogins.Where(l => l.UserInfo.UserName.Contains(userName));
            }
            #endregion
            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempLogins = Sort(tempLogins, e => e.AddTime, order).ThenBy(e => e.ID);
            }
            else
            {
                tempLogins = Sort(tempLogins, u => u.ID, order);
            }
            #endregion

            int totalCount = tempLogins.Count();
            var logins = LoginInfoServices
                .LoadPageEntities(pageIndex, pageSize, tempLogins);

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = logins.Select(l => new
                {
                    l.ID,
                    l.IP,
                    l.OS,
                    l.Device,
                    l.UserAgent,
                    l.AddTime,
                    l.UserInfo.UserName
                })
            });
        }
    }
}