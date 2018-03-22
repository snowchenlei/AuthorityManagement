using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class BaseEntity
    {
        public DateTime? AddTime { get; set; } = DateTime.Now;
    }
}
