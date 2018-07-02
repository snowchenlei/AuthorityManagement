using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cl.AuthorityManagement.Api.Controllers.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private IModuleServices ModuleServices = null;
        public ModuleController(IModuleServices moduleServices)
        {
            ModuleServices = moduleServices;
        }

        public IActionResult Modules(int pageIndex,int pageSize, string sort,
            OrderType order)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);

            var tempModules = ModuleServices.LoadEntities(m => true);

            #region 查询
            //if (!String.IsNullOrEmpty(moduleName))
            //{
            //    tempModules = tempModules.Where(u => u.Name.Contains(moduleName.Trim()));
            //}
            //if (parentId > 0)
            //{
            //    tempModules = tempModules.Where(u => u.Parent.ID == parentId);
            //}
            //if (startTime > new DateTime(1970, 1, 1) && startTime != endTime)
            //{
            //    tempModules = tempModules.Where(u => u.AddTime > startTime);
            //}
            //if (endTime > startTime)
            //{
            //    tempModules = tempModules.Where(u => u.AddTime < endTime);
            //}
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

            int totalCount = tempModules.Count();
            var modules = ModuleServices
                .LoadPageEntities(pageIndex, pageSize, tempModules);

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Ok(new Result<Object>
            {
                State = 1,
                Message = "获取成功",
                Data = new {
                    total = totalCount,
                    rows = modules.Select(m => new
                    {
                        ID = m.ID,
                        m.Name,
                        m.Url,
                        m.IconName,
                        m.Sort,
                        ParentID = m.Parent == null ? 0 : m.Parent.ID,
                        ParentName = m.Parent == null ? String.Empty : m.Parent.Name,
                        m.AddTime
                    })
                }
            });
        }

        protected IOrderedQueryable<T> Sort<T, S>(IQueryable<T> resource, Expression<Func<T, S>> orderbyLamada, OrderType orderType)
        {
            IOrderedQueryable<T> result = null;
            switch (orderType)
            {
                default:
                case OrderType.ASC:
                    result = resource.OrderBy(orderbyLamada);
                    break;
                case OrderType.DESC:
                    result = resource.OrderByDescending(orderbyLamada);
                    break;
            }
            return result;
        }
    }
}