using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Common.Encryption;
using Cl.AuthorityManagement.Common.Extension;
using Cl.AuthorityManagement.Common.Http;
using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserInfoServices UserInfoServices = null;
        private readonly ILoginInfoServices LoginInfoServices = null;
        public AccountController(
            IUserInfoServices userInfoServices,
            ILoginInfoServices loginInfoServices,
            AuthorityManagementContext context)
        {
            UserInfoServices = userInfoServices;
            LoginInfoServices = loginInfoServices;
        }

        // GET: Account
        public ActionResult Login()
        {
            try
            {
                string userInfo = "";//CookieHelper.GetCookieValue("user");
                if (!String.IsNullOrEmpty(userInfo))
                {
                    JObject jobj = JObject.Parse(userInfo);
                    string userName = jobj["UserName"]?.ToString(),
                        password = jobj["Password"]?.ToString();
                    UserInfo user = UserInfoServices
                        .LoadFirst(entity => entity.UserName == userName
                            && entity.Password == password);
                    if (user != null)
                    {
                        SetUser(user);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin login)
        {
            if (!ModelState.IsValid)
            {   //数据验证失败
                login.UserName = null;
                login.Password = null;
                return View();
            }
            if (!string.Equals(HttpContext.Session.Get<string>("verCode")
                , login.VerifyCode, StringComparison.InvariantCultureIgnoreCase))
            {
                ModelState.AddModelError("VerifyCode", "验证码错误");
                return View();
            }

            login.Password = Md5Encryption.Encrypt(Md5Encryption.Encrypt(login.Password, Md5EncryptionType.Strong));
            UserInfo userInfo = UserInfoServices
                .LoadFirst(entity => entity.UserName == login.UserName
                    && entity.Password == login.Password);

            if (userInfo == null)
            {
                ModelState.AddModelError("Password", "用户名与密码不匹配");
                return View();
            }
            if (userInfo.IsCanUse == false)
            {
                ModelState.AddModelError("", "当前用户不可用");
                return View();
            }
            SetUser(userInfo, login.RememberMe);       
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            //CookieHelper.ClearCookie("user");
            //Session["LoginUser"] = null;
            HttpContext.Session.Remove("LoginUser");
            return Json(new Result
            {
                State = 1,
                Message = "登出成功"
            });
        }

        private void SetUser(UserInfo userInfo, bool rememberMe = false)
        {
            if (rememberMe)
            {
                string cookieValue = Serialization.SerializeObject(new
                {
                    UserName = userInfo.UserName,
                    Password = userInfo.Password
                });
                //CookieHelper.SetCookie("user", cookieValue);
            }
            //Session["LoginUser"] = userInfo;
            HttpContext.Session.Set("LoginUser", userInfo);
            //UAParserUserAgent userAgent = new UAParserUserAgent(HttpContext);
            LoginInfoServices.AddEntity(new LoginInfo
            {
                IP = IPHelper.GetRealIP(HttpContext),
                //Device = userAgent.Device.ToString(),
                //OS = userAgent.OS.ToString(),
                //UserAgent = userAgent.UserAgent.ToString(),
                AddTime = DateTime.Now,
                UserInfo = userInfo
            });
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        public FileResult GetVerifyCodeImage()
        {
            string code = string.Empty;
            byte[] buffer = VerifyCode.Create(4, out code);
            HttpContext.Session.Set("verCode", code);
            return File(buffer, @"image/jpeg");
        }
    }
}