using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class RequestInfoController : AuthorizationController
    {
        private readonly IRequestInfoServices RequestInfoServices = null;
        public RequestInfoController(
            IRequestInfoServices requestInfoServices)
        {
            RequestInfoServices = requestInfoServices;
        }

        // GET: Manager/RequestInfo
        [OutputCache(Duration = 120, VaryByCustom = "Index_Key")]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Load(int pageIndex, int pageSize, string sort,
            OrderType order, string header, string requestMessage)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);

            var tempReqs = RequestInfoServices.LoadEntities(r => true);
            #region 查询
            if (!String.IsNullOrEmpty(header))
            {
                tempReqs = tempReqs.Where(r => r.Head.Contains(header));
            }
            if (!String.IsNullOrEmpty(requestMessage))
            {
                tempReqs = tempReqs.Where(r => r.RequestMessage.Contains(requestMessage));
            }
            #endregion
            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempReqs = Sort(tempReqs, r => r.AddTime, order).ThenBy(r => r.Id);
            }
            else
            {
                tempReqs = Sort(tempReqs, r => r.Id, order);
            }
            #endregion
            int totalCount = tempReqs.Count();
            var reqs = RequestInfoServices
                .LoadPageEntities(pageIndex, pageSize, tempReqs);

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = reqs.Select(r => new
                {
                    r.Id,
                    r.Url,
                    r.Head,
                    r.Message,
                    r.RequestMessage,
                    r.ResponseMessage,
                    r.AddTime
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}