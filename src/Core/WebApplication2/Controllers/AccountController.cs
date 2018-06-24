using Microsoft.AspNetCore.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IUserInfoServices UserInfoServices = null;
        //private readonly ILoginInfoServices LoginInfoServices = null;
        //public AccountController(
        //    IUserInfoServices userInfoServices,
        //    ILoginInfoServices loginInfoServices)
        //{
        //    UserInfoServices = userInfoServices;
        //    LoginInfoServices = loginInfoServices;
        //}

        // GET: Account
        public ActionResult Login()
        {
            return View();
            //try
            //{
            //    string userInfo = "";//CookieHelper.GetCookieValue("user");
            //    if (!String.IsNullOrEmpty(userInfo))
            //    {
            //        JObject jobj = JObject.Parse(userInfo);
            //        string userName = jobj["UserName"]?.ToString(),
            //            password = jobj["Password"]?.ToString();
            //        UserInfo user = UserInfoServices
            //            .LoadFirst(entity => entity.UserName == userName
            //                && entity.Password == password);
            //        if (user != null)
            //        {
            //            SetUser(user);
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //    return View();
            //}
            //catch (Exception ex)
            //{
            //    return View();
            //}
        }
        
    }
}