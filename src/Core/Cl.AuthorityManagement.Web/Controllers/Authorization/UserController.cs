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
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class UserController : AuthorizationController
    {
        private readonly IMapper Mapper = null;
        private readonly IRoleServices RoleServices = null;
        private readonly IModuleServices ModuleServices = null;
        private readonly IUserInfoServices UserInfoServices = null;
        private readonly IModuleElementServices ModuleElementServices = null;
        private readonly IUserInfoModuleElementServices UserInfoModuleElementServices = null;
        public UserController(
            IMapper mapper,
            IRoleServices roleServices,
            IModuleServices moduleServices,
            IUserInfoServices userInfoServices,
            IModuleElementServices moduleElementServices,
            IUserInfoModuleElementServices userInfoModuleElementServices)
        {
            Mapper = mapper;
            RoleServices = roleServices;
            ModuleServices = moduleServices;
            UserInfoServices = userInfoServices;
            ModuleElementServices = moduleElementServices;
            UserInfoModuleElementServices = userInfoModuleElementServices;
        }

        // GET: User
        [ResponseCache(CacheProfileName = "Header")]
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
                tempUsers = Sort(tempUsers, u => u.AddTime, order).ThenBy(u => u.ID);
            }
            else
            {
                tempUsers = Sort(tempUsers, u => u.ID, order);
            } 
            #endregion
            int totalCount = tempUsers.Count();
            var users = UserInfoServices
                .LoadPageEntities(pageIndex, pageSize, tempUsers);

            int pageCount = PageHelper.GetPageCount(totalCount, pageSize);
            return Json(new
            {
                total = totalCount,
                rows = users.Select(u => new
                {
                    Id = u.ID,
                    u.UserName,
                    u.Name,
                    u.PhoneNumber,
                    u.Password,
                    u.AddTime,
                    u.IsCanUse
                })
            });
        }

        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        [ResponseCache(CacheProfileName = "Default")]
        public ActionResult Add()
        {
            //ViewBag.Action = "Add";
            //ViewBag.Operate = "添加";
            return PartialView("Edit");
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Add(UserEdit userEdit)
        {
            if (ModelState.IsValid)
            {
                UserInfo user = Mapper.Map<UserInfo>(userEdit);
                user.Password = Md5Encryption.Encrypt(Md5Encryption.Encrypt(user.Password, Md5EncryptionType.Strong));
                user = UserInfoServices.AddEntity(user);

                //LoggerHelper.Operate(new OperateLog
                //{
                //    CreateUser_Id = UserInfo.ID,
                //    OperateType = (int)OperateType.Add,
                //    Remark = $"{UserInfo.Name}添加了一个用户{userEdit.Name}"
                //});
                return Json(new Result<int>
                {
                    State = 1,
                    Message = "添加成功",
                    Data = user.ID
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
                UserInfo user = UserInfoServices
                        .LoadFirst(u => u.ID == userEdit.ID.Value);
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
        public ActionResult Roles(int userID)
        {
            CheckReturn check = UserInfoServices
                .LoadRoles(userID);
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
        public ActionResult Roles(int firstID, string secondID)
        {
            string[] tempIds = secondID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] roleIds = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            ReturnDescription description = UserInfoServices
                .SetUserRole(firstID, roleIds);

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

        #region 设置模块
        [HttpGet]
        [AjaxOnly]
        [Authenticate]
        public ActionResult Modules(int userID)
        {
            CheckReturn check = UserInfoServices
                .LoadModules(userID);
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
            string[] tempIds = secondID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] moduleIDs = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            ReturnDescription description = UserInfoServices
                 .SetUserModule(firstID, moduleIDs);
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
        public ActionResult ModuleElements(int userID)
        {
            if (!UserInfoServices.IsExists(u => u.ID == userID))
            {
                return Json(new
                {
                    State = 0,
                    Message = "用户不存在"
                });
            }
            List<Module> modules = UserInfoServices.LoadUserModule(userID);
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
        public ActionResult ModuleElements(int userID, string elementID, int moduleID)
        {
            string[] tempIds = elementID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] elementIds = Array.ConvertAll(tempIds, s => Convert.ToInt32(s));

            ReturnDescription description = UserInfoServices.SetUserModuleElements(userID, elementIds, moduleID);
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
        public string Elements(int userID, int moduleID)
        {
            CheckReturn check = UserInfoServices
                .LoadModuleElements(userID, moduleID);
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