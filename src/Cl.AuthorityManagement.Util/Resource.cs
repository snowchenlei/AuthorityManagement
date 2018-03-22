using Cl.AuthorityManagement.Model.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Util
{
    public static class Resource
    {
        public static Dictionary<string, PageInfo> PageInfos { get
            {
                return new Dictionary<string, PageInfo>
                {
                    ["UserSetRole"] = new PageInfo
                    {
                        Title = "为用户分配角色",
                        Url = "\"/User/Roles\""
                    },
                    ["UserSetModule"] = new PageInfo
                    {
                        Title = "为用户分配模块",
                        Url = "\"/User/Modules\""
                    },
                    ["UserSetElement"] = new PageInfo
                    {
                        Title = "为用户分配元素",
                        Url = "\"/User/ModuleElements\""
                    },
                    ["RoleSetModule"] = new PageInfo
                    {
                        Title = "为角色分配模块",
                        Url = "\"/Role/Modules\""
                    },
                    ["RoleSetElement"] = new PageInfo
                    {
                        Title = "为角色分配元素",
                        Url = "\"/Role/ModuleElements\""
                    },
                    ["ModuleSetElement"] = new PageInfo
                    {
                        Title = "为模块分配元素",
                        Url = "\"/Module/ModuleElements\""
                    }
                };
            } }

        public static Queue<KeyValuePair<Exception, object>> ApiErrorQueue =
            new Queue<KeyValuePair<Exception, object>>();
        public static Queue<Exception> MvcErrorQueue = new Queue<Exception>();
    }
}
