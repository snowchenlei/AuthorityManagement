using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cl.AuthorityManagement.Web.Controllers.Log
{
    public class MonitorInfoController : AuthorizationController
    {
        private readonly IMonitorInfoServices MonitorInfoServices = null;
        public MonitorInfoController(
            IMonitorInfoServices monitorInfoServices)
        {
            MonitorInfoServices = monitorInfoServices;
        }

        // GET: Manager/MonitorInfo
        [ResponseCache(CacheProfileName = "Header")]
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
           string requestMessage, string responseMessage, string httpMethod, int application)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);

            var tempMonitors = MonitorInfoServices.LoadEntities(r => true);
            #region 查询
            if (!String.IsNullOrEmpty(controllerName))
            {
                tempMonitors = tempMonitors.Where(r => r.Controller.Contains(controllerName));
            }
            if (!String.IsNullOrEmpty(actionName))
            {
                tempMonitors = tempMonitors.Where(r => r.Action.Contains(actionName));
            }
            if (!String.IsNullOrEmpty(requestMessage))
            {
                tempMonitors = tempMonitors.Where(r => r.RequestMessage.Contains(requestMessage));
            }
            if (!String.IsNullOrEmpty(responseMessage))
            {
                tempMonitors = tempMonitors.Where(r => r.ResponseMessage.Contains(responseMessage));
            }
            if (!String.IsNullOrEmpty(httpMethod))
            {
                tempMonitors = tempMonitors.Where(r => r.HttpMethod.Equals(httpMethod, StringComparison.InvariantCultureIgnoreCase));
            }
            if (application > -1)
            {
                tempMonitors = tempMonitors.Where(r => r.ApplicationType == (ApplicationType)application);
            }
            #endregion
            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempMonitors = Sort(tempMonitors, r => r.AddTime, order).ThenBy(r => r.ID);
            }
            if ("SumTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempMonitors = Sort(tempMonitors, r => r.SumTime, order).ThenBy(r => r.ID);
            }
            else
            {
                tempMonitors = Sort(tempMonitors, r => r.ID, order);
            }
            #endregion
            int totalCount = tempMonitors.Count();
            var monitors = MonitorInfoServices
                .LoadPageEntities(pageIndex, pageSize, tempMonitors);

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = monitors.Select(m => new
                {
                    m.ID,
                    m.Head,
                    m.StartTime,
                    m.EndTime,
                    m.SumTime,
                    m.Controller,
                    m.Action,
                    m.HttpMethod,
                    m.IP,
                    m.Description,
                    m.RequestMessage,
                    m.ResponseMessage,
                    m.ApplicationType,
                    m.AddTime
                })
            });
        }
    }
}