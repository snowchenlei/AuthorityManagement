using Cl.AuthorityManagement.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class MonitorInfo : Entity
    {
        [StringLength(500)]
        public string Controller { get; set; }
        [StringLength(500)]
        public string Action { get; set; }
        public double SumTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [StringLength(50)]
        public string HttpMethod { get; set; }
        
        [StringLength(500)]
        public string Logger { get; set; }
        public string Description { get; set; }
        [StringLength(2000)]
        public string Head { get; set; }
        [StringLength(500)]
        public string RequestMessage { get; set; }
        [StringLength(500)]
        public string ResponseMessage { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        public ApplicationType ApplicationType { get; set; }
    }
}
