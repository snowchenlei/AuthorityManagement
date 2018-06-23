using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.Model
{
    public class CheckReturn
    {
        public bool Flag { get; set; }
        public string Message { get; set; }
        public Dictionary<int, string> dics { get; set; }
        public int[] IDs { get; set; }
    }
}
