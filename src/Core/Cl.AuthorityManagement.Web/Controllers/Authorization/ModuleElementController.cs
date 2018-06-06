using AutoMapper;
using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Library.Mvc;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class ModuleElementController : AuthorizationController
    {
        private readonly IModuleElementServices ModuleElementServices = null;
        public ModuleElementController(
            IModuleElementServices moduleElementServices)
        {
            ModuleElementServices = moduleElementServices;
        }

        // GET: ModuleElement
        [ResponseCache(CacheProfileName = "Header")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Load(int pageIndex, int pageSize, string sort,
            OrderType order, string name, DateTime startTime, DateTime endTime)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);
            IQueryable<ModuleElement> tempElements = ModuleElementServices.LoadEntities(e => true);

            #region 查询
            if (!String.IsNullOrEmpty(name))
            {
                tempElements = tempElements.Where(u => u.Name.Contains(name.Trim()));
            }            
            if (startTime > new DateTime(1970, 1, 1) && startTime != endTime)
            {
                tempElements = tempElements.Where(u => u.AddTime > startTime);
            }
            if (endTime > startTime)
            {
                tempElements = tempElements.Where(u => u.AddTime < endTime);
            }
            #endregion

            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempElements = Sort(tempElements, e => e.AddTime, order).ThenBy(e => e.ID);
            }
            else if ("Sort".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempElements = Sort(tempElements, e => e.Sort, order).ThenBy(e => e.ID);
            }
            else
            {
                tempElements = Sort(tempElements, u => u.ID, order);
            }
            #endregion
            var roles = ModuleElementServices
                .LoadPageEntities(pageIndex, pageSize, tempElements);
            int totalCount = roles.Count();
            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = roles.Select(u => new
                {
                    u.ID,
                    u.DomId,
                    u.Attr,
                    u.Class,
                    u.Icon,
                    u.Remark,
                    u.Script,
                    u.Type,
                    u.Sort,
                    u.Name,
                    u.Action,
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
        public ActionResult Add(ModuleElementEdit moduleElementEdit)
        {
            if (ModelState.IsValid)
            {
                ModuleElement moduleElement = Mapper.Map<ModuleElement>(moduleElementEdit);
                ModuleElementServices.AddEntity(moduleElement);
                return Json(new Result<int>
                {
                    State = 1,
                    Message = "添加成功",
                    Data = moduleElement.ID
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
        public ActionResult Edit(ModuleElementEdit moduleElementEdit)
        {
            if (ModelState.IsValid)
            {
                ModuleElement moduleElement = ModuleElementServices
                    .LoadFirst(r => r.ID == moduleElementEdit.Id.Value);
                if (moduleElement == null)
                {
                    return Json(new Result
                    {
                        State = 0,
                        Message = "修改的角色不存在"
                    });
                }
                moduleElement = Mapper.Map(moduleElementEdit, moduleElement);
                if (ModuleElementServices.EditEntity(moduleElement))
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
                        State = 0,
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
        [Authenticate]
        public ActionResult Delete(int id)
        {
            ModuleElement moduleElement = ModuleElementServices
                .LoadFirst(u => u.ID == id);
            if (moduleElement != null)
            {
                ModuleElementServices.DeleteEntity(moduleElement);
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
                    Message = "模块元素不存在"
                });
            }
        }
    }
}