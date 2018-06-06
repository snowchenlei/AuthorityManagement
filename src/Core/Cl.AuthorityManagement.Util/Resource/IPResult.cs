using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Common.Http;
using Cl.AuthorityManagement.Entity;
using Newtonsoft.Json.Linq;

namespace Cl.AuthorityManagement.Util
{
    public class IPResult
    {
        public static IPInfo GetData(string ip)
        {
            string result = GetReult(ip);
            return Check(result);
        }

        public static string GetReult(string ip)
        {
            string result = HttpServer.HttpPost(Resource.ThirdUrl["ip"], "ip=" + ip);
            return result;
        }
        public static IPInfo Check(string result)
        {
            //IPRootObject rootObject = Serialization.DeserializeObject<IPRootObject>(result);
            JObject jobj = JObject.Parse(result);
            if (jobj["code"]?.ToString() == "0")
            {
                return Serialization.DeserializeObject<IPInfo>(jobj["data"]);
            }
            return null;
        }
    }
}
