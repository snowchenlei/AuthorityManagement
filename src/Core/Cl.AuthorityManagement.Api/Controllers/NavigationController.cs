using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cl.AuthorityManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        private readonly IModuleServices ModuleServices = null;
        private readonly IUserInfoServices UserInfoServices = null;
        public NavigationController(
            IModuleServices moduleServices,
            IUserInfoServices userInfoServices)
        {
            ModuleServices = moduleServices;
            UserInfoServices = userInfoServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            UserInfo user = UserInfoServices.LoadFirst(u => u.UserName == "admin");
            List<Module> modules = ModuleServices.LoadSelectModules(user);
            return Ok(new Result<object>
            {
                State = 1,
                Data = modules.Select(m => new
                {
                    m.ID,
                    parentID = m.Parent?.ID,
                    m.Name,
                    m.IconName,
                    m.Sort
                })
            });
        }
    }
}