using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model
{
    public class Result<T>
    {
        /// <summary>
        /// 0失败,1成功,2特殊操作
        /// </summary>
        public int State { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class Result
    {
        public int State { get; set; }
        public string Message { get; set; }
    }
}
