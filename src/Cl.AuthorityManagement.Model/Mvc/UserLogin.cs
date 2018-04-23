using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model.Mvc
{
    /// <summary>
    /// 后台登陆
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码")]
        [Required(ErrorMessage = "请输入{0}")]
        public string VerifyCode { get; set; }
    }
}
