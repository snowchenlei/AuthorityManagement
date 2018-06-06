using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Util
{
    public class LoadHtml
    {
        /// <summary>
        /// 获取元素集合
        /// </summary>
        /// <param name="options">所有的元素</param>
        /// <param name="ids">已拥有的元素</param>
        /// <returns>html</returns>
        public static string GetElements(Dictionary<int, string> options, int[] ids)
        {
            StringBuilder sbHtml = new StringBuilder();
            foreach (KeyValuePair<int, string> item in options)
            {
                sbHtml.AppendLine("<label class='checkbox-inline' style='margin-left: 0px; margin-right: 10px;'>");
                if (ids.Contains(item.Key))
                {
                    sbHtml.AppendFormat("<input type='checkbox' class='elements' id='Name' name='Name' value='{0}' data-id='{0}' checked='checked' /> <span class='chkSp'>{1}</span>\n", item.Key, item.Value);
                }
                else
                {
                    sbHtml.AppendFormat("<input type='checkbox' class='elements' id='Name' name='Name' value='{0}' data-id='{0}' /> <span class='chkSp'>{1}</span>\n", item.Key, item.Value);
                }
                sbHtml.AppendLine("</label>");
            }
            return sbHtml.ToString();
        }
    }
}
