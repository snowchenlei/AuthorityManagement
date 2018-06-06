using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model.Logger
{
     public class OperateLog
    {
        public int CreateUser_Id { get; set; }
        public int OperateType { get; set; }
        public string Remark { get; set; }
    }
}
