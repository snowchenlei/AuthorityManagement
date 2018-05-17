using Cl.AuthorityManagement.Model.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Linq;

namespace Cl.AuthorityManagement.Util
{
    public static class Resource
    {
        public static Queue<KeyValuePair<Exception, object>> ApiErrorQueue =
            new Queue<KeyValuePair<Exception, object>>();
        public static Queue<KeyValuePair<Exception, object>> MvcErrorQueue =
           new Queue<KeyValuePair<Exception, object>>();

        /// <summary>
        /// 视图页信息
        /// </summary>
        public static Dictionary<string, PageInfo> PageInfos
        {
            get
            {
                return new Dictionary<string, PageInfo>
                {
                    #region 权限管理
                    ["Role"] = new PageInfo
                    {
                        Title = "角色管理",
                        Url = "/Role/Index",
                        AbsoluteUrl = "/Role/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    },
                    ["User"] = new PageInfo
                    {
                        Title = "用户管理",
                        Url = "/User/Index",
                        AbsoluteUrl = "/User/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    },
                    ["Module"] = new PageInfo
                    {
                        Title = "模块管理",
                        Url = "/Module/Index",
                        AbsoluteUrl = "/Module/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    },
                    ["ModuleElement"] = new PageInfo
                    {
                        Title = "模块元素管理",
                        Url = "/ModuleElement/Index",
                        AbsoluteUrl = "/ModuleElement/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    },
                    ["UserSetRole"] = new PageInfo
                    {
                        Title = "为用户分配角色",
                        Url = "\"/User/Roles\"",
                        AbsoluteUrl = "/User/",
                        Layout = "~/Views/Shared/_LayoutCheckElement.cshtml"
                    },
                    ["UserSetModule"] = new PageInfo
                    {
                        Title = "为用户分配模块",
                        Url = "\"/User/Modules\"",
                        AbsoluteUrl = "/User/",
                        Layout = "~/Views/Shared/_LayoutCheckElement.cshtml"
                    },
                    ["UserSetElement"] = new PageInfo
                    {
                        Title = "为用户分配元素",
                        Url = "\"/User/ModuleElements\"",
                        AbsoluteUrl = "/User/",
                        Layout = "~/Views/Shared/_LayoutCheckElement.cshtml"
                    },
                    ["RoleSetModule"] = new PageInfo
                    {
                        Title = "为角色分配模块",
                        Url = "\"/Role/Modules\"",
                        AbsoluteUrl = "/Role/",
                        Layout = "~/Views/Shared/_LayoutCheckElement.cshtml"
                    },
                    ["RoleSetElement"] = new PageInfo
                    {
                        Title = "为角色分配元素",
                        Url = "\"/Role/ModuleElements\"",
                        AbsoluteUrl = "/Role/",
                        Layout = "~/Views/Shared/_LayoutCheckElement.cshtml"
                    },
                    ["ModuleSetElement"] = new PageInfo
                    {
                        Title = "为模块分配元素",
                        Url = "\"/Module/ModuleElements\"",
                        AbsoluteUrl = "/Module/",
                        Layout = "~/Views/Shared/_LayoutCheckElement.cshtml"
                    },
                    #endregion                    
                    #region 日志
                    ["RequestInfo"] = new PageInfo
                    {
                        Title = "请求日志管理",
                        Url = "/RequestInfo/Index",
                        AbsoluteUrl = "/RequestInfo/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    },
                    ["MonitorInfo"] = new PageInfo
                    {
                        Title = "来访日志管理",
                        Url = "/MonitorInfo/Index",
                        AbsoluteUrl = "/MonitorInfo/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    },
                    ["ErrorInfo"] = new PageInfo
                    {
                        Title = "来访日志管理",
                        Url = "/ErrorInfo/Index",
                        AbsoluteUrl = "/ErrorInfo/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    },
                    ["LoginInfo"] = new PageInfo
                    {
                        Title = "登陆日志管理",
                        Url = "/LoginInfo/Index",
                        AbsoluteUrl = "/LoginInfo/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    },
                    ["OperateInfo"] = new PageInfo
                    {
                        Title = "操作日志管理",
                        Url = "/OperateInfo/Index",
                        AbsoluteUrl = "/OperateInfo/",
                        Layout = "~/Views/Shared/_LayoutPageTable.cshtml"
                    }
                    #endregion
                };
            }
        }

        /// <summary>
        /// 第三方url
        /// </summary>
        public static Dictionary<string, string> ThirdUrl
        {
            get
            {
                return new Dictionary<string, string>
                {
                    ["ip"] = "http://ip.taobao.com/service/getIpInfo2.php"
                };
            }
        }

        /// <summary>
        /// 通用正则
        /// </summary>
        public static Dictionary<string, string> Regexs
        {
            get
            {
                string path = HostingEnvironment.MapPath("~/Config/RegexConfig.xml");
                XDocument document = XDocument.Load(path);
                return document.Descendants("regex")
                    .Select(x => new
                    {
                        key = x.Element("key").Value,
                        value = x.Element("value").Value
                    }).ToDictionary(key => key.key, value => value.value);
            }
        }
    }
}
