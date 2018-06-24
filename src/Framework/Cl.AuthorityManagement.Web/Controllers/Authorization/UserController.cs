using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Cl.AuthorityManagement.Common;
using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Common.Encryption;
using Cl.AuthorityManagement.Common.Logger;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Library.Mvc;
using Cl.AuthorityManagement.Model;
using Cl.AuthorityManagement.Model.Logger;
using Cl.AuthorityManagement.Model.Mvc;
using Cl.AuthorityManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class UserController : AuthorizationController
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
        [OutputCache(Duration = 120, VaryByCustom = "Index_Key")]
        public ActionResult Index()
        {
            return View();
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
            if(startTime > new DateTime(1970, 1, 1) && startTime != endTime)
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
                tempUsers = Sort(tempUsers, u => u.AddTime, order).ThenBy(u => u.Id);
            }
            else
            {
                tempUsers = Sort(tempUsers, u => u.Id, order);
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
                    u.Name,
                    u.PhoneNumber,
                    u.Password,
                    u.AddTime,
                    u.IsCanUse
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
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserEdit userEdit)
        {
            if (ModelState.IsValid)
            {
                UserInfo user = Mapper.Map<UserInfo>(userEdit);
                user.Password = Md5Encryption.Encrypt(Md5Encryption.Encrypt(user.Password, Md5EncryptionType.Strong));
                UserInfoServices.AddEntity(user);

                LoggerHelper.Operate(new OperateLog
                {
                    CreateUser_Id = UserInfo.Id,
                    OperateType = (int)OperateType.Add,
                    Remark = $"{UserInfo.Name}添加了一个用户{userEdit.Name}"
                });
                return Json(new Result<int>
                {
                    State = 1,
                    Message = "添加成功",
                    Data = user.Id
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

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="managerAgent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEdit userEdit)
        {
            if (ModelState.IsValid)
            {
                UserInfo user = UserInfoServices
                        .LoadFirst(u => u.Id == userEdit.Id.Value);
                if (user == null)
                {
                    return Json(new Result
                    {
                        State = 0,
                        Message = "修改的用户不存在"
                    });
                }
                user = Mapper.Map(userEdit, user);
                if (UserInfoServices.EditEntity(user))
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
        [AjaxOnly]
        [Authenticate]
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
        [Authenticate]
        [ValidateAntiForgeryToken]
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
        [AjaxOnly]
        [Authenticate]
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
        [Authenticate]
        [ValidateAntiForgeryToken]
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
        [AjaxOnly]
        [Authenticate]
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
        [Authenticate]
        [ValidateAntiForgeryToken]
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