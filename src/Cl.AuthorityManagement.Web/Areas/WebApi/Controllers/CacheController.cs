using Cl.AuthorityManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Cl.AuthorityManagement.Web.Areas.WebApi.Controllers
{
    public class CacheController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ClearIndex_Key()
        {
            HttpRuntime.Cache.Remove("Index_Key");
            return Json(new Result
            {
                State = 1,
                Message = "删除成功"
            });
        }
    }
}
