using Cl.AuthorityManagement.Library.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class PushController : AuthorizationController
    {
        // GET: Push
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Push(string message)
        {
            return View();
        }
    }
}