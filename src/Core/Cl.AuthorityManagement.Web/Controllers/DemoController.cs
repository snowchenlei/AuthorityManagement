using AutoMapper;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class DemoController : BaseController
    {
        private readonly IMapper Mapper = null;
        private readonly IModuleServices ModuleServices = null;
        public DemoController(
            IMapper mapper,
            IModuleServices moduleServices)
        {
            Mapper = mapper;
            ModuleServices = moduleServices;
        }

        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }
    }
}