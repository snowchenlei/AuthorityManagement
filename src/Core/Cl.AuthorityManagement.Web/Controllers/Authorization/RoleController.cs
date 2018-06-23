using AutoMapper;
using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Library.Mvc;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Mvc;
using Cl.AuthorityManagement.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class RoleController : AuthorizationController
    {
        private readonly IRoleServices RoleServices = null;
        private readonly IModuleServices ModuleServices = null;
        private readonly IRoleModuleElementServices RoleModuleElementServices = null;
        public RoleController(
            IRoleServices roleServices,
            IModuleServices moduleServices,
            IRoleModuleElementServices roleModuleElementServices)
        {
            RoleServices = roleServices;
            ModuleServices = moduleServices;
            RoleModuleElementServices = roleModuleElementServices;
        }

        // GET: Role
        [ResponseCache(CacheProfileName = "Header")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Load(int pageIndex, int pageSize, string sort,
            OrderType order, string roleName,DateTime startTime, DateTime endTime)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);
            int totalCount;
            var tempRoles = RoleServices.LoadEntities(r => true);
            #region 查询
            if (!String.IsNullOrEmpty(roleName))
            {
                tempRoles = tempRoles.Where(r => r.Name.Contains(roleName.Trim()));
            }
            if (startTime > new DateTime(1970, 1, 1) && startTime != endTime)
            {
                tempRoles = tempRoles.Where(r => r.AddTime > startTime);
            }
            if (endTime > startTime)
            {
                tempRoles = tempRoles.Where(r => r.AddTime < endTime);
            }
            #endregion

            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempRoles = Sort(tempRoles, r => r.AddTime, order).ThenBy(r => r.ID);
            }
            else if ("Sort".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempRoles = Sort(tempRoles, r => r.Sort, order).ThenBy(r => r.ID);
            }
            else
            {
                tempRoles = Sort(tempRoles, r => r.ID, order);
            }
            #endregion

            //var roles = RoleServices
            //    .LoadPageEntities(pageIndex, pageSize, role);
            var roles = RoleServices.LoadPageEntities(pageIndex, pageSize, tempRoles);
            totalCount = tempRoles.Count();

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = roles.Select(u => new
                {
                    Id = u.ID,
                    u.Sort,
                    u.Name,
                    u.AddTime
                })
            });
        }

        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        [ResponseCache(CacheProfileName = "Default")]
        public ActionResult Add()
        {
            return PartialView("Edit");
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Add(RoleEdit roleEdit)
        {
            if (ModelState.IsValid)
            {
                Role role = Mapper.Map<Role>(roleEdit);
                //role.IsDelete = 0;
                RoleServices.AddEntity(role);
                return Json(new Result<int>
                {
                    State = 1,
                    Message = "添加成功",
                    Data = role.ID
                });
            }
            else
            {
                IEnumerable<object> errors = ModelStateToJson();
                return Json(new Result<object>
                {
                    State = 0,
                    Message = "错误",
                    Data = errors
                });
            }
        }

        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        [ResponseCache(CacheProfileName = "Default")]
        public ActionResult Edit()
        {
            return PartialView();
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Edit(RoleEdit roleEdit)
        {
            if (ModelState.IsValid)
            {
                Role role = RoleServices
                    .LoadFirst(r => r.ID == roleEdit.ID.Value);
                if (role == null)
                {
                    return Json(new Result
                    {
                        State = 0,
                        Message = "修改的角色不存在"
                    });
                }
                role = Mapper.Map(roleEdit, role);
                if (RoleServices.EditEntity(role))
                {
                    return Json(new Result
                    {
                        State = 1,
                        Message = "修改成功"
                    });
                }
                else
                {
                    return Json(new Result
                    {
                        State = 1,
                        Message = "修改失败"
                    });
                }
            }
            else
            {
                IEnumerable<object> errors = ModelStateToJson();
                return Json(new Result<object>
                {
                    State = 0,
                    Message = "错误",
                    Data = errors
                });
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {

            if (RoleServices.Delete(id))
            {
                return Json(new Result
                {
                    State = 1,
                    Message = "删除成功"
                });
            }
            else
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "删除失败"
                });
            }
        }

        #region 设置模块
        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        public ActionResult Modules(int roleID)
        {
            CheckReturn check = RoleServices
                .LoadModules(roleID);
            if (check.Flag)
            {
                ViewBag.Html = LoadHtml.GetElements(check.dics, check.IDs);
                return PartialView();
            }
            else
            {
                return Json(new
                {
                    State = 0,
                    Message = check.Message
                });
            }
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Modules(int firstID, string secondID)
        {
            string[] tempIDs = secondID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] moduleIDs = Array.ConvertAll(tempIDs, s => Convert.ToInt32(s));
            
            ReturnDescription description = RoleServices
                 .SetRoleModule(firstID, moduleIDs);
            if (description.Flag)
            {
                return Json(new Result
                {
                    State = 1,
                    Message = description.Message
                });
            }
            else
            {
                return Json(new Result
                {
                    State = 0,
                    Message = description.Message
                });
            }
        }
        #endregion

        #region 设置元素
        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        public ActionResult ModuleElements(int roleID)
        {
            if (!RoleServices.IsExists(u => u.ID == roleID))
            {
                return Json(new
                {
                    State = 0,
                    Message = "角色不存在"
                });
            }
            List<Module> modules = RoleServices
                .LoadRoleModule(roleID);
            if (modules.Count <= 0)
            {
                ViewBag.ModuleTree = "\"\"";
            }
            else
            {
                ViewBag.ModuleTree = Serialization.SerializeObject(modules
                    .Select(m => new
                    {
                        id = m.ID,
                        pId = m.Parent?.ID,
                        name = m.Name
                    }));
            }
            return PartialView();
        }

        [HttpPost]
        [Authenticate]
        public ActionResult ModuleElements(int roleID, string elementID, int moduleID)
        {
            string[] tempIDs = elementID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] elementIDs = Array.ConvertAll(tempIDs, s => Convert.ToInt32(s));

            ReturnDescription description = RoleServices
                 .SetRoleModuleElements(roleID, elementIDs, moduleID);
            if (description.Flag)
            {
                return Json(new Result
                {
                    State = 1,
                    Message = description.Message
                });
            }
            else
            {
                return Json(new Result
                {
                    State = 0,
                    Message = description.Message
                });
            }
        }

        [HttpGet]
        [AjaxOnly]
        public string Elements(int roleID, int moduleID)
        {
            CheckReturn check = RoleServices
                .LoadModuleElements(roleID, moduleID);
            if (check.Flag)
            {
                return LoadHtml.GetElements(check.dics, check.IDs);
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion
    }
}