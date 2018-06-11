using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class PushController : AuthorizationController
    {
        private readonly IPushServices PushServices = null;
        public PushController(
            IPushServices pushServices)
        {
            PushServices = pushServices;
        }

        // GET: Push
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Load(int pageIndex, int pageSize, string sort,
            OrderType order, string phoneNumber, string userName, DateTime startTime, DateTime endTime)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);

            var tempPushes = PushServices.LoadEntities(u => true);
            #region 查询

            #endregion
            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempPushes = Sort(tempPushes, u => u.AddTime, order).ThenBy(u => u.Id);
            }
            else
            {
                tempPushes = Sort(tempPushes, u => u.Id, order);
            }
            #endregion

            var pushes = PushServices
                .LoadPageEntities(pageIndex, pageSize, tempPushes);
            int totalCount = pushes.Count();

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = pushes.Select(p => new
                {
                    p.Id,
                    p.Message,
                    SourceUserName = p.SourceUser.Name,
                    TargetUserName = p.SourceUser.Name,
                    p.AddTime
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Push()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Push(PushModel model)
        {
            return Json(new Result<string>
            {
                State = 1,
                Message = "保存成功",
                Data = model.Message
            });
        }
    }
}