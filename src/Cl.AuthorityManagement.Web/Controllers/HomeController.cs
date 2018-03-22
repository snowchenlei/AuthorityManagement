using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
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
            #region 加载当前用户列表
            List<Module> modules = ModuleServices.LoadSelectModules(userInfo);
            #endregion
            moduleList = modules;
            ViewData["Module"] = GetNav(modules.Where(n => n.Parent == null),
                new StringBuilder());
            ViewBag.Name = String.IsNullOrEmpty(userInfo.Name) ? "至尊" : userInfo.Name;
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public string MenuHeader()
        {
            StringBuilder sb = new StringBuilder();
            int moduleId = ModuleServices
                .LoadEntities(m => m.Url.IndexOf(Controllername) > 0)
                .Select(m => m.Id)
                .FirstOrDefault();
            int[] roleIds = userInfo.Roles
                .Select(r => r.Id)
                .ToArray();

            List<ModuleElement> moduleElements = ModuleElementServices
                .LoadSelectElements(userInfo.Id, moduleId, roleIds);

            sb.AppendLine("<div class='btnOperation'>");
            foreach (ModuleElement element in moduleElements)
            {
                // data-toggle='modal' data-target='#modifyModal'

                //sb.AppendFormat("<button id='{0}' class='btn btn-sm {1}' onclick='{2}' {3}>\r\n"
                //    , element.DomId, element.Class, element.Script, element.Attr);
                sb.AppendFormat("<button id='{0}' class='btn btn-sm {1}' {2}>\r\n"
                    , element.DomId, element.Class, element.Attr);
                if (!string.IsNullOrEmpty(element.Icon))
                {
                    sb.AppendFormat("<i class='glyphicon glyphicon-{0}' style='margin-right: 5px;'></i>", element.Icon);
                }
                sb.Append(element.Name + "</button>");
            }
            sb.AppendLine("</div>");
            return sb.ToString();

        }

        public ActionResult Welcome()
        {
            return View();
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
                    sbHtml.AppendFormat("<i class='menu-icon fa {0}'></i>\r\n", module.IconName);
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