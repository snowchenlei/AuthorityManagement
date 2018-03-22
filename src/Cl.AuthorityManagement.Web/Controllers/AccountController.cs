using Cl.AuthorityManagement.Entity;
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
    public class AccountController : Controller
    {
        private readonly IUserInfoServices UserInfoServices = null;
        public AccountController(
            IUserInfoServices userInfoServices)
        {
            UserInfoServices = userInfoServices;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            if (!ModelState.IsValid)
            {   //数据验证失败
                login.UserName = null;
                login.Password = null;
                return View();
            }
            //if (Session["verCode"] == null || !string.Equals(Session["verCode"].ToString()
            //    , login.VerifyCode, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    ModelState.AddModelError("", "验证码错误");
            //    return View();
            //}
            UserInfo userInfo = UserInfoServices.LoadFirst(
                entity => entity.UserName == login.UserName
                && entity.Password == login.Password);

            if (userInfo == null)
            {
                ModelState.AddModelError("", "用户名与密码不匹配");
                return View();
            }
            Session["LoginUser"] = userInfo;            
            return RedirectToAction("Index", "Home");
        }
    }
}