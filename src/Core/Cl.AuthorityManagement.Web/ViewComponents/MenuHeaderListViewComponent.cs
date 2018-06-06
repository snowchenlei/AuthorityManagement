using Cl.AuthorityManagement.Common.Extension;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Web.ViewComponents
{
    public class MenuHeaderListViewComponent:ViewComponent
    {
        private readonly IModuleServices ModuleServices = null;
        private readonly IModuleElementServices ModuleElementServices = null;
        public MenuHeaderListViewComponent(
            IModuleServices moduleServices
            ,IModuleElementServices moduleElementServices)
        {
            ModuleServices = moduleServices;
            ModuleElementServices = moduleElementServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int moduleID)
        {
            try
            {
                moduleID = Convert.ToInt32(HttpContext.Request.QueryString.Value.Split('=')[1]);
            }
            catch(Exception ex)
            {
                return View("");
            }
            var items = await GetItemsAsync(moduleID);
            return View(items);
        }
        private async Task<List<ModuleElement>> GetItemsAsync(int moduleID)
        {
            UserInfo user = HttpContext.Session.Get<UserInfo>("LoginUser");

            List<ModuleElement> moduleElements = ModuleElementServices
                .LoadModuleElement(user.ID, moduleID);
            return moduleElements;
        }
    }
}
