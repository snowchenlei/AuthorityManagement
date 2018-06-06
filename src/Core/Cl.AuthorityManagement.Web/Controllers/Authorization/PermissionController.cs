using AutoMapper;
using Cl.AuthorityManagement.Common;
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
    /// <summary>
    /// 权限
    /// </summary>
    public class PermissionController : Controller
    {
        private readonly IPermissionServices PermissionServices = null;
        public PermissionController(
            IPermissionServices permissionServices)
        {
            PermissionServices = permissionServices;
        }

        // GET: Permission
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Load(int pageIndex, int pageSize)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);
            int totalCount;
            var users = PermissionServices
                .LoadPageEntities(pageIndex, pageSize, out totalCount,
                    u => true, u => u.Id);

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = users.Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Sort,
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
        public ActionResult Edit(PermissionEdit permissionEdit)
        {
            if (ModelState.IsValid)
            {
                if (permissionEdit.Id.HasValue)
                {
                    Permission permission = PermissionServices
                        .LoadFirst(u => u.Id == permissionEdit.Id.Value);
                    if (permission == null)
                    {
                        return Json(new Result
                        {
                            State = 0,
                            Message = "修改的权限不存在"
                        });
                    }

                    permission.Name = permissionEdit.Name?.ToString();
                    permission.Sort = permissionEdit.Sort;
                    PermissionServices.EditEntity(permission);
                    return Json(new Result
                    {
                        State = 1,
                        Message = "修改成功"
                    });
                }
                else
                {
                    Permission permission = Mapper.Map<Permission>(permissionEdit);
                    permission.AddTime = DateTime.Now;
                    PermissionServices.AddEntity(permission);

                    return Json(new Result<int>
                    {
                        State = 1,
                        Message = "添加成功",
                        Data = permission.Id
                    });
                }
            }
            return PartialView(permissionEdit);
        }
    }
}