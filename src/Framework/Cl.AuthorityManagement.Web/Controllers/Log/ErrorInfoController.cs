using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class ErrorInfoController : AuthorizationController
    {
        private readonly IErrorInfoServices ErrorInfoServices = null;
        public ErrorInfoController(
            IErrorInfoServices errorInfoServices)
        {
            ErrorInfoServices = errorInfoServices;
        }

        [OutputCache(Duration = 120, VaryByCustom = "Index_Key")]
        public ActionResult Index()
        {
            InitSelect();
            return View();
        }

        public void InitSelect()
        {
            ViewBag.Applications = Serialization.SerializeObject(EnumToSelect(typeof(ApplicationType)));
        }

        public JsonResult Load(int pageIndex, int pageSize, string sort,
           OrderType order, string controllerName, string actionName,
           string requestMessage, string exception, string httpMethod, int application)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);

            var tempErrors = ErrorInfoServices.LoadEntities(r => true);
            #region 查询
            if (!String.IsNullOrEmpty(controllerName))
            {
                tempErrors = tempErrors.Where(r => r.Controller.Contains(controllerName));
            }
            if (!String.IsNullOrEmpty(actionName))
            {
                tempErrors = tempErrors.Where(r => r.Action.Contains(actionName));
            }
            if (!String.IsNullOrEmpty(requestMessage))
            {
                tempErrors = tempErrors.Where(r => r.RequestMessage.Contains(requestMessage));
            }
            if (!String.IsNullOrEmpty(exception))
            {
                tempErrors = tempErrors.Where(r => r.Exception.Contains(exception));
            }
            if (!String.IsNullOrEmpty(httpMethod))
            {
                tempErrors = tempErrors.Where(r => r.HttpMethod.Equals(httpMethod, StringComparison.InvariantCultureIgnoreCase));
            }
            if (application > -1)
            {
                tempErrors = tempErrors.Where(r => r.ApplicationType == (ApplicationType)application);
            }
            #endregion
            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempErrors = Sort(tempErrors, r => r.AddTime, order).ThenBy(r => r.Id);
            }
            else
            {
                tempErrors = Sort(tempErrors, r => r.Id, order);
            }
            #endregion
            int totalCount = tempErrors.Count();
            var errors = ErrorInfoServices
                .LoadPageEntities(pageIndex, pageSize, tempErrors);

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = errors.Select(m => new
                {
                    m.Id,
                    m.Head,
                    m.Controller,
                    m.Action,
                    m.HttpMethod,
                    m.IP,
                    m.Description,
                    m.RequestMessage,
                    m.ApplicationType,
                    m.Exception,
                    m.InnerException,
                    m.AddTime
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}