using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model.Mvc
{
    public class UserEdit
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11, MinimumLength = 11,ErrorMessage = "手机号长度为11位")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "请输入正确的手机号")]
        [Display(Name = "手机号")]
        public string PhoneNumber { get; set; }

        [Display(Name = "是否可用")]
        public bool IsCanUse { get; set; }
    }
}
