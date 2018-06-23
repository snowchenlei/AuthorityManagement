using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class RequestInfo : Entity
    {
        [StringLength(500)]
        public string Logger { get; set; }
        public string Message { get; set; }
        [StringLength(500)]
        public string Head { get; set; }
        [StringLength(500)]
        public string Url { get; set; }
        public string RequestMessage { get; set; }
        public string ResponseMessage { get; set; }
    }
}
