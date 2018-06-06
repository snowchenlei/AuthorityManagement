using AutoMapper;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class IPController : AuthorizationController
    {
        private readonly IIPInfoServices IPInfoServices = null;
        public IPController(
            IIPInfoServices iPInfoServices)
        {
            IPInfoServices = iPInfoServices;
        }

        // GET: Manager/IP
        [OutputCache(Duration = 120, VaryByCustom = "Index_Key")]
        public ActionResult Index(string ip)
        {
            Regex regex = new Regex(Resource.Regexs["ip"]);
            IPDetails details = null;
            IPInfo ipInfo = null;
            if (regex.IsMatch(ip))
            {
                ipInfo = IPInfoServices.LoadFirst(i => i.IP == ip);
                if (ipInfo == null)
                {
                    ipInfo = IPResult.GetData(ip);
                    //IPData ipData = IPResult.GetData(ip);
                    //IPInfo ipInfoMapp = Mapper.Map<IPInfo>(ipData);
                    ipInfo = IPInfoServices.AddEntity(ipInfo);
                }
            }
            details = Mapper.Map<IPDetails>(ipInfo);
            return View(details);
        }
    }
}