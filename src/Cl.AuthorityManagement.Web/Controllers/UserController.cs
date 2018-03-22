using AutoMapper;
using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Mvc;
using Cl.AuthorityManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserInfoServices UserInfoServices = null;
        private readonly IRoleServices RoleServices = null;
        private readonly IModuleServices ModuleServices = null;
        private readonly IModuleElementServices ModuleElementServices = null;
        private readonly IUserInfoModuleElementServices UserInfoModuleElementServices = null;
        public UserController(
            IUserInfoServices userInfoServices,
            IRoleServices roleServices,
            IModuleServices moduleServices,
            IModuleElementServices moduleElementServices,
            IUserInfoModuleElementServices userInfoModuleElementServices)
        {
            UserInfoServices = userInfoServices;
            RoleServices = roleServices;
            ModuleServices = moduleServices;
            ModuleElementServices = moduleElementServices;
            UserInfoModuleElementServices = userInfoModuleElementServices;
        }

        // GET: User
        public ActionResult Index()
        {
            InitSelect();
            return View();
        }

        /// <summary>
        /// 加载下拉数据
        /// </summary>
        public void InitSelect()
        {
        }

        public JsonResult Load(int pageIndex, int pageSize, string sort,
            OrderType order, string phoneNumber,string userName,DateTime startTime,DateTime endTime)
        {
            PageHelper.GetPageIndex(ref pageIndex);
            PageHelper.GetPageSize(ref pageSize);

            var tempUsers = UserInfoServices.LoadEntities(u => true);
            #region 查询
            if (!String.IsNullOrEmpty(phoneNumber))
            {
                tempUsers = tempUsers.Where(u => u.PhoneNumber == phoneNumber.Trim());
            }
            if (!String.IsNullOrEmpty(userName))
            {
                tempUsers = tempUsers.Where(u => u.UserName == userName.Trim());
            }
            if(startTime > new DateTime(1970, 1, 1))
            {
                tempUsers = tempUsers.Where(u => u.AddTime > startTime);
            }
            if (endTime > startTime)
            {
                tempUsers = tempUsers.Where(u => u.AddTime < endTime);
            }
            #endregion

            #region 排序
            if ("AddTime".Equals(sort, StringComparison.InvariantCultureIgnoreCase))
            {
                switch (order)
                {
                    default:
                    case OrderType.ASC:
                        tempUsers = tempUsers.OrderBy(u => u.AddTime).ThenBy(u => u.Id);
                        break;
                    case OrderType.DESC:
                        tempUsers = tempUsers.OrderByDescending(u => u.AddTime).ThenBy(u => u.Id);
                        break;
                }
            }
            else
            {
                tempUsers = tempUsers.OrderBy(u => u.Id);
            } 
            #endregion
            var users = UserInfoServices
                .LoadPageEntities(pageIndex, pageSize, tempUsers);
            int totalCount = users.Count();

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = users.Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.PhoneNumber,
                    u.Password,
                    u.AddTime,
                    u.IsCanUse
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[ChildActionOnly]
        public ActionResult Edit()
        {
            return PartialView();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="managerAgent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserEdit userEdit)
        {
            if (ModelState.IsValid)
            {
                if (userEdit.Id.HasValue)
                {
                    UserInfo user = UserInfoServices
                        .LoadFirst(u => u.Id == userEdit.Id.Value);
                    if(user == null)
                    {
                        return Json(new Result
                        {
                            State = 0,
                            Message = "修改的用户不存在"
                        });
                    }

                    user.UserName = userEdit.UserName?.Trim();
                    user.Password = userEdit.Password?.Trim();
                    user.PhoneNumber = userEdit.PhoneNumber?.Trim();
                    user.IsCanUse = userEdit.IsCanUse;
                    UserInfoServices.EditEntity(user);
                    return Json(new Result
                    {
                        State = 1,
                        Message = "修改成功"
                    });
                }
                else
                {
                    UserInfo user = Mapper.Map<UserInfo>(userEdit);
                    user.AddTime = DateTime.Now;
                    UserInfoServices.AddEntity(user);
                    
                    return Json(new Result<int>
                    {
                        State = 1,
                        Message = "添加成功",
                        Data = user.Id
                    });
                }
            }
            return PartialView(userEdit);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (UserInfoServices.Delete(id))
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

        #region 设置角色
        [HttpGet]
        public ActionResult Roles(int userId)
        {
            UserInfo user = UserInfoServices
                .LoadFirst(u => u.Id == userId);
            if (user != null)
            {
                var roles = RoleServices.LoadEntities(c => true)
                    .Select(r => new
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToDictionary(key => key.Id, value => value.Name);
                int[] ids = user.Roles.Select(r => r.Id).ToArray();
                
                ViewBag.Html = LoadHtml.GetElements(roles, ids);
                return PartialView();
            }
            else
            {
                return Json(new
                {
                    State = 0,
                    Message = "用户不存在"
                });
            }
        }
        [HttpPost]
        public ActionResult Roles(int firstId, string secondId)
        {
            string[] tempIds = secondId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] roleIds = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            UserInfo user = UserInfoServices
                .LoadFirst(u => u.Id == firstId);
            if (user == null)
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置角色的用户不存在"
                });
            }

            if (UserInfoServices.SetUserRole(user, roleIds))
            {
                return Json(new Result
                {
                    State = 1,
                    Message = "设置角色成功"
                });
            }
            else
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置角色失败"
                });
            }
        }
        #endregion

        #region 设置模块
        [HttpGet]
        public ActionResult Modules(int userId)
        {
            UserInfo user = UserInfoServices
                .LoadFirst(u => u.Id == userId);
            if (user != null)
            {
                var modules = ModuleServices.LoadEntities(m => true)
                    .Select(r => new
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToDictionary(key => key.Id, value => value.Name);
                int[] ids = user.Modules.Select(r => r.Id).ToArray();
                
                ViewBag.Html = LoadHtml.GetElements(modules, ids);
                return PartialView();
            }
            else
            {
                return Json(new
                {
                    State = 0,
                    Message = "用户不存在"
                });
            }
        }
        [HttpPost]
        public ActionResult Modules(int firstId, string secondId)
        {
            string[] tempIds = secondId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] moduleIds = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            UserInfo user = UserInfoServices
                .LoadFirst(u => u.Id == firstId);
            if (user == null)
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置模块的用户不存在"
                });
            }

            if (UserInfoServices.SetUserModule(user, moduleIds))
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
        public ActionResult ModuleElements(int userId)
        {
            UserInfo user = UserInfoServices
                .LoadFirst(u => u.Id == userId);
            if (user != null)
            {
                ViewBag.ModuleTree = GetModuleTreeJson(user);
                return PartialView();
            }
            else
            {
                return Json(new
                {
                    State = 0,
                    Message = "用户不存在"
                });
            }
        }
        [HttpPost]
        public ActionResult ModuleElements(int userId, string elementId, int moduleId)
        {
            string[] tempIds = elementId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] elementIds = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            UserInfo user = UserInfoServices
                .LoadFirst(u => u.Id == userId);
            if (user == null)
            {
                return Json(new Result
                {
                    State = 0,
                    Message = "设置元素的用户不存在"
                });
            }

            if (UserInfoServices.SetUserModuleElements(user, elementIds, moduleId))
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

        private string GetModuleTreeJson(UserInfo user)
        {
            List<Module> modules = new List<Module>();
            modules.AddRange(user.Modules);
            foreach (Role role in user.Roles)
            {
                modules.AddRange(role.Modules);
            }

            return Serialization.SerializeObject(modules
                .Select(m => new
                {
                    id = m.Id,
                    pId = m.Parent?.Id,
                    name = m.Name
                }));
        }

        [HttpGet]
        public string Elements(int userId, int moduleId)
        {
            Module module = ModuleServices
                .LoadFirst(m => m.Id == moduleId);

            int[] ids = UserInfoModuleElementServices
                .LoadEntities(e => e.Module.Id == moduleId
                    && e.UserInfo.Id == userId)
                .Select(e => e.ModuleElement.Id).ToArray();

            var elements = module.ModuleElements
                .Select(e => new
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToDictionary(key => key.Id, value => value.Name);

            return LoadHtml.GetElements(elements, ids);
        }
        #endregion
    }
}