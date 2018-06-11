using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    /// <summary>
    /// 推送
    /// </summary>
    public class Push : Entity
    {
        [Required]
        [MinLength(10)]
        public string Message { get; set; }

        [Required]
        [JsonIgnore]
        public virtual UserInfo SourceUser { get; set; }
        [Required]
        [JsonIgnore]
        public virtual ICollection<UserInfo> TargetUser { get; set; }
    }
}
