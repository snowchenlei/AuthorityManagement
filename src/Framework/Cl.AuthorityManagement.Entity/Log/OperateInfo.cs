using Cl.AuthorityManagement.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class OperateInfo: Entity
    {
        public OperateType OperateType { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        public UserInfo CreateUser { get; set; }
    }
}
