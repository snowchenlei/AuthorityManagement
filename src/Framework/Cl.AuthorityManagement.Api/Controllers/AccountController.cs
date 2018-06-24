using Cl.AuthorityManagement.Common.Encryption;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cl.AuthorityManagement.Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserInfoServices UserInfoServices = null;
        private readonly ILoginInfoServices LoginInfoServices = null;
        public AccountController(
            IUserInfoServices userInfoServices,
            ILoginInfoServices loginInfoServices)
        {
            UserInfoServices = userInfoServices;
            LoginInfoServices = loginInfoServices;
        }

        [HttpPost]
        public IHttpActionResult Login(string username, string password)
        {
            password = Md5Encryption.Encrypt(Md5Encryption.Encrypt(password, Md5EncryptionType.Strong));
            UserInfo userInfo = UserInfoServices
                .LoadFirst(entity => entity.UserName == username
                    && entity.Password == password);
            if (userInfo == null)
            {                
                return Json(new Result
                {
                     State = 0,
                     Message = "用户名或密码错误"
                });
            }
            if (userInfo.IsCanUse == false)
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "当前用户不可用"
                });
            }
            return Json(new Result
            {
                State = 1,
                Message = "登陆成功"
            });
        }

        [HttpGet]
        public IHttpActionResult Logout()
        {
            return Json(new Result
            {
                State = 1,
                Message = "登出成功"
            });
        }
    }
}
