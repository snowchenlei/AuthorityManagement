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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public class ModuleController : AuthorizationController
    {
        private readonly IModuleServices ModuleServices = null;
        private readonly IModuleElementServices ModuleElementServices = null;
        public ModuleController(
            IModuleServices moduleServices,
            IModuleElementServices moduleElementServices)
        {
            ModuleServices = moduleServices;
            ModuleElementServices = moduleElementServices;
        }

        // GET: Module
        [OutputCache(Duration = 120, VaryByCustom = "Index_Key")]
        public ActionResult Index()
        {
            InitSelect();
            return View();
        }

        private void InitSelect()
        {
            var modules = ModuleServices
                .LoadEntities(m => true)
                .Select(m => new
                {
                    id = m.Id,
                    text = m.Name
                })
                .ToList();
            modules.Insert(0, new { id = -1, text = "请选择" });
            ViewBag.Modules = Serialization.SerializeObject(modules);
        }

        public JsonResult Load(int pageIndex, int pageSize, string sort,
            OrderType order, string moduleName, int parentId,
            DateTime startTime, DateTime endTime)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);

            var tempModules = ModuleServices.LoadEntities(m => true);
            #region 查询
            if (!String.IsNullOrEmpty(moduleName))
            {
                tempModules = tempModules.Where(u => u.Name.Contains(moduleName.Trim()));
            }
            if (parentId>0)
            {
                tempModules = tempModules.Where(u => u.Parent.Id == parentId);
            }
            if (startTime > new DateTime(1970, 1, 1) && startTime != endTime)
            {
                tempModules = tempModules.Where(u => u.AddTime > startTime);
            }
            if (endTime > startTime)
            {
                tempModules = tempModules.Where(u => u.AddTime < endTime);
            }
            #endregion

            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempModules = Sort(tempModules, r => r.AddTime, order).ThenBy(r => r.Id);
            }
            else if ("Sort".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempModules = Sort(tempModules, r => r.Sort, order).ThenBy(r => r.Id);
            }
            else
            {
                tempModules = Sort(tempModules, r => r.Id, order);
            }
            #endregion
            var modules = ModuleServices
                .LoadPageEntities(pageIndex, pageSize, tempModules);
            int totalCount = modules.Count();

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = modules.Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.Url,
                    m.IconName,
                    m.Sort,
                    ParentId = m.Parent == null ? 0 : m.Parent.Id,
                    ParentName = m.Parent == null ? String.Empty : m.Parent.Name,
                    m.AddTime
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        [OutputCache(Duration = 120)]
        public ActionResult Add()
        {
            return PartialView("Edit");
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Add(ModuleEdit moduleEdit)
        {
            if (ModelState.IsValid)
            {
                Module parent = null;
                if (moduleEdit.ParentId.HasValue && moduleEdit.ParentId.Value > 0)
                {
                    parent = ModuleServices
                        .LoadFirst(m => m.Id == moduleEdit.ParentId);
                }
                Module module = Mapper.Map<Module>(moduleEdit);
                module.AddTime = DateTime.Now;
                if (parent != null)
                {
                    module.Parent = parent;
                }
                ModuleServices.AddEntity(module);

                return Json(new Result<int>
                {
                    State = 1,
                    Message = "添加成功",
                    Data = module.Id
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
        [OutputCache(Duration = 120)]
        public ActionResult Edit()
        {
            return PartialView();
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Edit(ModuleEdit moduleEdit)
        {
            if (ModelState.IsValid)
            {
                Module parent = null;
                if (moduleEdit.ParentId.HasValue)
                {
                    parent = ModuleServices
                        .LoadFirst(m => m.Id == moduleEdit.ParentId);
                }
                Module module = ModuleServices
                        .LoadFirst(u => u.Id == moduleEdit.Id.Value);
                if (module == null)
                {
                    return Json(new Result
                    {
                        State = 0,
                        Message = "修改的用户不存在"
                    });
                }
                module = Mapper.Map(moduleEdit, module);
                module.Name = moduleEdit.Name?.Trim();
                module.Url = moduleEdit.Url?.Trim();
                module.IconName = moduleEdit.IconName?.Trim();
                module.Sort = moduleEdit.Sort;
                if (parent != null)
                {
                    module.Parent = parent;
                }
                if (ModuleServices.EditEntity(module))
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
            Module module = ModuleServices
                .LoadFirst(u => u.Id == id);
            if (module != null)
            {
                if (!ModuleServices
                    .IsExists(m => m.Parent.Id == module.Id))
                {
                    if (ModuleServices.DelectModule(module))
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
                else
                {
                    return Json(new Result
                    {
                        State = 0,
                        Message = "请先删除子元素后再试"
                    });
                }
            }
            else
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "用户不存在"
                });
            }
        }

        #region 设置模块元素
        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        public ActionResult ModuleElements(int moduleId)
        {
            Module module = ModuleServices
                .LoadFirst(m => m.Id == moduleId);
            if (module != null)
            {
                var moduleElements = ModuleElementServices.LoadEntities(c => true)
                   .Select(r => new
                   {
                       Id = r.Id,
                       Name = r.Name
                   }).ToDictionary(key => key.Id, value => value.Name);
                int[] ids = module.ModuleElements.Select(r => r.Id).ToArray();

                ViewBag.Html = LoadHtml.GetElements(moduleElements, ids);
                return PartialView();
            }
            else
            {
                return Json(new
                {
                    State = 0,
                    Message = "模块不存在"
                });
            }
        }

        [HttpPost]
        [Authenticate]
        public ActionResult ModuleElements(int firstId, string secondId)
        {
            string[] tempIds = secondId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] elementIds = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            Module module = ModuleServices
                .LoadFirst(r => r.Id == firstId);
            if (module == null)
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置元素的模块不存在"
                });
            }
            if (ModuleServices.SetModuleElements(module, elementIds))
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
        #endregion
    }
}