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
                    u.ID,
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
                    .LoadFirst(r => r.ID == roleEdit.Id.Value);
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
        public ActionResult Modules(int roleId)
        {
            Role role = RoleServices
                .LoadFirst(r => r.ID == roleId);
            if(role!= null)
            {
                var modules = ModuleServices.LoadEntities(c => true)
                    .Select(r => new
                    {
                        Id = r.ID,
                        Name = r.Name
                    }).ToDictionary(key => key.Id, value => value.Name);
                //角色已拥有的模块
                int[] ids = role.RoleModules.Select(r => r.MouleID).ToArray();

                ViewBag.Html = LoadHtml.GetElements(modules, ids);
                return PartialView();
            }
            else
            {
                return Json(new
                {
                    State = 0,
                    Message = "角色不存在"
                });
            }
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Modules(int firstId, string secondId)
        {
            string[] tempIds = secondId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] moduleIds = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            Role role = RoleServices
                .LoadFirst(r => r.ID == firstId);
            if (role == null)
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置模块的角色不存在"
                });
            }
            if (RoleServices.SetRoleModule(role, moduleIds))
            {
                return Json(new Result
                {
                    State = 1,
                    Message = "设置模块成功"
                });
            }
            else
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置模块失败"
                });
            }
        }
        #endregion

        #region 设置元素
        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        public ActionResult ModuleElements(int roleId)
        {
            Role role = RoleServices
                .LoadFirst(r => r.ID == roleId);
            if (role != null)
            {
                ViewBag.ModuleTree = GetModuleTreeJson(role);
                return PartialView();
            }
            else
            {
                return Json(new
                {
                    State = 0,
                    Message = "角色不存在"
                });
            }
        }

        [HttpPost]
        [Authenticate]
        public ActionResult ModuleElements(int roleId, string elementId, int moduleId)
        {
            string[] tempIds = elementId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] elementIds = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            Role role = RoleServices
                .LoadFirst(r => r.ID == roleId);
            if (role == null)
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置元素的角色不存在"
                });
            }

            if (RoleServices.SetRoleModuleElements(role, elementIds, moduleId))
            {
                return Json(new Result
                {
                    State = 1,
                    Message = "设置元素成功"
                });
            }
            else
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置元素失败"
                });
            }
        }
        
        private string GetModuleTreeJson(Role role)
        {
            return Serialization.SerializeObject(role.RoleModules.Select(r=>r.Module)
                .Select(m => new
                {
                    id = m.ID,
                    pId = m.Parent?.ID,
                    name = m.Name
                }));
        }

        [HttpGet]
        [AjaxOnly]
        public string Elements(int roleId, int moduleId)
        {
            Module module = ModuleServices
                .LoadFirst(m => m.ID == moduleId);
            int[] ids = RoleModuleElementServices
                .LoadEntities(e => e.Module.ID == moduleId
                    && e.Role.ID == roleId)
                .Select(e => e.ModuleElement.ID).ToArray();

            var elements = module.ModuleElementModules.Select(m=>m.ModuleElement)
                .Select(e => new
                {
                    Id = e.ID,
                    Name = e.Name
                })
                .ToDictionary(key => key.Id, value => value.Name);

            return LoadHtml.GetElements(elements, ids);
        }
        #endregion
    }
}