using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    interface IEntity : IEntity<int>
    {
    }
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey ID { get; set; }
        DateTime? AddTime { get; set; }
    }
}
