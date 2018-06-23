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
    /// <summary>
    /// 模块管理
    /// </summary>
    public class ModuleController : AuthorizationController
    {
        private readonly IMapper Mapper = null;
        private readonly IModuleServices ModuleServices = null;
        private readonly IModuleElementServices ModuleElementServices = null;
        public ModuleController(
            IMapper mapper,
            IModuleServices moduleServices,
            IModuleElementServices moduleElementServices)
        {
            Mapper = mapper;
            ModuleServices = moduleServices;
            ModuleElementServices = moduleElementServices;
        }

        // GET: Module
        [ResponseCache(CacheProfileName = "Header")]
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
                    id = m.ID,
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
                tempModules = tempModules.Where(u => u.Parent.ID == parentId);
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
                tempModules = Sort(tempModules, r => r.AddTime, order).ThenBy(r => r.ID);
            }
            else if ("Sort".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                tempModules = Sort(tempModules, r => r.Sort, order).ThenBy(r => r.ID);
            }
            else
            {
                tempModules = Sort(tempModules, r => r.ID, order);
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
                    Id = m.ID,
                    m.Name,
                    m.Url,
                    m.IconName,
                    m.Sort,
                    ParentID = m.Parent == null ? 0 : m.Parent.ID,
                    ParentName = m.Parent == null ? String.Empty : m.Parent.Name,
                    m.AddTime
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
        public ActionResult Add(ModuleEdit moduleEdit)
        {
            if (ModelState.IsValid)
            {
                Module module = Mapper.Map<Module>(moduleEdit);
                //Module parent = null;
                if (moduleEdit.ParentID.HasValue && moduleEdit.ParentID.Value > 0)
                {
                    module.Parent = ModuleServices
                        .LoadFirst(m => m.ID == moduleEdit.ParentID);
                }
                module.AddTime = DateTime.Now;
                //if (parent != null)
                //{
                //    module.Parent = parent;
                //}
                ModuleServices.AddEntity(module);

                return Json(new Result<int>
                {
                    State = 1,
                    Message = "添加成功",
                    Data = module.ID
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
        public ActionResult Edit(ModuleEdit moduleEdit)
        {
            if (ModelState.IsValid)
            {
                Module parent = null;
                if (moduleEdit.ParentID.HasValue)
                {
                    parent = ModuleServices
                        .LoadFirst(m => m.ID == moduleEdit.ParentID);
                }
                Module module = ModuleServices
                        .LoadFirst(u => u.ID == moduleEdit.ID.Value);
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
                .LoadFirst(u => u.ID == id);
            if (module != null)
            {
                if (!ModuleServices
                    .IsExists(m => m.Parent.ID == module.ID))
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
        public ActionResult ModuleElements(int moduleID)
        {
            CheckReturn check = ModuleServices
                 .LoadElements(moduleID);
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
        public ActionResult ModuleElements(int firstID, string secondID)
        {
            string[] tempIDs = secondID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] elementIDs = Array.ConvertAll(tempIDs, s => Convert.ToInt32(s));

            ReturnDescription description = ModuleServices.SetModuleElements(firstID, elementIDs);
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
    }
}