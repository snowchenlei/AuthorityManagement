using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Common.Http;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Library.Mvc;
using Cl.AuthorityManagement.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IModuleServices ModuleServices = null;
        private readonly IModuleElementServices ModuleElementServices = null;
        public HomeController(
            IModuleServices moduleServices,
            IModuleElementServices moduleElementServices)
        {
            ModuleServices = moduleServices;
            ModuleElementServices = moduleElementServices;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Name = String.IsNullOrEmpty(UserInfo.Name) ? "至尊" : UserInfo.Name;
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public string MenuHeader()
        {
            StringBuilder sb = new StringBuilder();
            int moduleId = ModuleServices
                .LoadEntities(m => m.Url.IndexOf("/" + ControllerName + "/") == 0)
                .Select(m => m.Id)
                .FirstOrDefault();
            int[] roleIds = UserInfo.Roles
                .Select(r => r.Id)
                .ToArray();

            List<ModuleElement> moduleElements = ModuleElementServices
                .LoadSelectElements(UserInfo.Id, moduleId, roleIds);
            
            foreach (ModuleElement element in moduleElements)
            {
                sb.AppendFormat("<button id='{0}' class='btn btn-sm {1}' data-action='{2}' {3}>\r\n"
                    , element.DomId, element.Class, element.Action, element.Attr);
                if (!string.IsNullOrEmpty(element.Icon))
                {
                    sb.AppendFormat("<i class='glyphicon {0}' style='margin-right: 5px;'></i>", element.Icon);
                }
                sb.Append(element.Name + "</button>");
            }
            return sb.ToString();

        }

        public string Weather()
        {
            string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=js";
            string result = HttpServer.HttpGet(url);
            // 请求到的原始string需要处理一下才能解析  
            result = result.Split('=')[1].Trim().TrimEnd(';');
            // 解析json字符串  
            JObject jobj = JObject.Parse(result);
            // 国家  
            string country = jobj["country"]?.ToString();
            // 省份  
            string province = jobj["province"]?.ToString();
            // 城市
            string city = jobj["city"]?.ToString();
            
            result = HttpServer.HttpGet("http://wthrcdn.etouch.cn/weather_mini?city=" + HttpUtility.UrlEncode(city.Replace("市", "")));

            return "";
        }
        
        public ActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// 导航列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult Navs()
        {
            List<Module> modules = ModuleServices.LoadSelectModules(UserInfo);
            moduleList = modules;
            string result = GetNav(modules.Where(n => n.Parent == null), new StringBuilder());
            return Json(new Result<string>
            {
                State = 1,
                Data = result
            }, JsonRequestBehavior.AllowGet);
        }

        List<Module> moduleList = new List<Module>();
        private string GetNav(IEnumerable<Module> modules, StringBuilder sbHtml)
        {
            foreach (Module module in modules)
            {
                sbHtml.AppendLine("<li>");
                var currentNavs = moduleList
                    .Where(n => n.Parent != null
                        && n.Parent.Id == module.Id)
                    .OrderBy(m => m.Sort);
                if (currentNavs.Count() > 0)
                {
                    sbHtml.AppendLine("<a href='#' class='dropdown-toggle'>");
                    sbHtml.AppendFormat("<i class='menu-icon glyphicon {0}'></i>\r\n", module.IconName);
                    sbHtml.AppendFormat("<span class='menu-text'>{0}</span>\r\n", module.Name);
                    sbHtml.AppendLine("<b class='arrow glyphicon glyphicon-chevron-down'></b>");
                    sbHtml.AppendLine("</a>");
                    sbHtml.AppendLine("<b class='arrow'></b>");
                    sbHtml.AppendLine("<ul class='submenu'>");
                    GetNav(currentNavs, sbHtml);
                    sbHtml.AppendLine("</ul>");
                }
                else
                {//target='contentFrame_{1}'
                    sbHtml.AppendFormat("<a class='deepNav' href='{0}?moduleId={1}' data-id='{1}' data-name='{2}' target='tab_{1}' >\r\n", module.Url, module.Id, module.Name);
                    sbHtml.AppendFormat("<i class='menu-icon glyphicon {0}'></i>\r\n", module.IconName);
                    sbHtml.AppendFormat("<span class='menu-text'>{0}</span>\r\n", module.Name);
                    sbHtml.AppendLine("</a>");
                    sbHtml.AppendLine("<b class='arrow'></b>");
                }
                sbHtml.AppendLine("</li>");
            }
            return sbHtml.ToString();
        }
    }
}