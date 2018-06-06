using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    /// <summary>
    /// 拥有一个int主键
    /// </summary>
    public abstract class Entity : Entity<int>, IEntity<int>
    {        
    }
    public abstract class Entity<TPrimaryKey>
    {
        public int ID { get; set; }
        public DateTime? AddTime { get; set; } = DateTime.Now;
        
        public override string ToString()
        {
            return $"[{GetType().Name} {ID}]";
        }
    }
}
