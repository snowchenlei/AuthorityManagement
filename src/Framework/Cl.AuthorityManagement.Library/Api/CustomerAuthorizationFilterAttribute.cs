using Qm.CardManagement.Common.Encryption;
using Qm.CardManagement.Common.Time;
using Qm.CardManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Cl.AuthorityManagement.Library.Api
{
    /// <summary>
    /// Api权限处理
    /// </summary>
    public class CustomerAuthorizationFilterAttribute: AuthorizationFilterAttribute
    {
        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
#if DEBUG
            goto a;
#endif
            HttpRequest request = HttpContext.Current.Request;

            if (Resource.Type == 1)
            {
                Resource.Type = -1;
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"请求地址不存在\"}", Encoding.UTF8, "application/json"),
                    RequestMessage = actionContext.Request
                };
                return;
            }
            string md5Key = Resource.Md5Key;


            // 请求地址必须为全部小写（参数除外）
            if (Regex.IsMatch(actionContext.Request.RequestUri.AbsolutePath, "[A-Z]"))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"请求地址不存在\"}", Encoding.UTF8, "application/json"),
                    RequestMessage = actionContext.Request
                };
                return;
            }
            #region 认证
            string transTimeSpan = request.Headers["TrasnTimeSpan"];
            string transType = request.Headers["TransType"];
            string transKey = request.Headers["TransKey"];
            #region 数据校验
            if (transTimeSpan == null || transType == null || transKey == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("{\"nResul\":0,\"Message\":\"请求数据不匹配\"}", Encoding.UTF8, "application/json"),
                    RequestMessage = actionContext.Request
                };
                return;
            }
            #endregion

            #region 时间校验
            goto b;
            DateTime sendTime = TimeHelper.TimestampToDateTime(long.Parse(transTimeSpan));
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = sendTime - now;
            
            if (timeSpan.TotalMinutes > 2 || timeSpan.TotalMinutes < -2)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("{\"nResul\":0,\"Message\":\"请求超时\"}", Encoding.UTF8, "application/json"),
                    RequestMessage = actionContext.Request
                };
                return;
            }
            b:
            #endregion

            #region 签名认证
            string pwdKey = Md5Encryption.Encrypt(transType + transTimeSpan + md5Key);
            if (!pwdKey.Equals(transKey, StringComparison.InvariantCultureIgnoreCase))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"签名不正确\"}", Encoding.UTF8, "application/json"),
                    RequestMessage = actionContext.Request
                };
                return;
            }
            #endregion
            #endregion
            a:
            base.OnAuthorization(actionContext);
        }
    }
}
