using AutoMapper;
using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class ModuleElementController : BaseController
    {
        private readonly IModuleElementServices ModuleElementServices = null;
        public ModuleElementController(
            IModuleElementServices moduleElementServices)
        {
            ModuleElementServices = moduleElementServices;
        }

        // GET: ModuleElement
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
            if (startTime > new DateTime(1970, 1, 1))
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
                tempElements = Sort(tempElements, e => e.AddTime, order).ThenBy(e => e.Id);
            }
            else if ("Sort".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempElements = Sort(tempElements, e => e.Sort, order).ThenBy(e => e.Id);
            }
            else
            {
                tempElements = Sort(tempElements, u => u.Id, order);
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
                    u.Id,
                    u.DomId,
                    u.Attr,
                    u.Class,
                    u.Icon,
                    u.Remark,
                    u.Script,
                    u.Type,
                    u.Sort,
                    u.Name,
                    u.AddTime
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Edit(ModuleElementEdit moduleElementEdit)
        {
            if (ModelState.IsValid)
            {
                if (moduleElementEdit.Id.HasValue)
                {
                    ModuleElement moduleElement = ModuleElementServices
                        .LoadFirst(r => r.Id == moduleElementEdit.Id.Value);
                    if (moduleElement == null)
                    {
                        return Json(new Result
                        {
                            State = 0,
                            Message = "修改的角色不存在"
                        });
                    }
                    //moduleElement = Mapper.Map<ModuleElement>(moduleElementEdit);
                    moduleElement.Attr = moduleElementEdit.Attr;
                    moduleElement.Class = moduleElementEdit.Class;
                    moduleElement.DomId = moduleElementEdit.DomId;
                    moduleElement.Icon = moduleElementEdit.Icon;
                    moduleElement.Remark = moduleElementEdit.Remark;
                    moduleElement.Name = moduleElementEdit.Name;
                    moduleElement.Sort = moduleElementEdit.Sort;
                    moduleElement.Script = moduleElementEdit.Script;
                    ModuleElementServices.EditEntity(moduleElement);
                    return Json(new Result
                    {
                        State = 1,
                        Message = "修改成功"
                    });
                }
                else
                {
                    ModuleElement moduleElement = Mapper.Map<ModuleElement>(moduleElementEdit);
                    ModuleElementServices.AddEntity(moduleElement);
                    return Json(new Result<int>
                    {
                        State = 1,
                        Message = "添加成功",
                        Data = moduleElement.Id
                    });
                }
            }
            return PartialView();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            ModuleElement moduleElement = ModuleElementServices
                .LoadFirst(u => u.Id == id);
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