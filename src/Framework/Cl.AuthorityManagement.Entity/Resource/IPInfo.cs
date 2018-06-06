using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class IPInfo
    {
        public int Id { get; set; }
        [StringLength(50)]
        [JsonProperty("ip")]
        public string IP { get; set; }

        [StringLength(50)]
        [JsonProperty("country")]
        public string Country { get; set; }

        [StringLength(50)]
        [JsonProperty("area")]
        public string Area { get; set; }

        [StringLength(50)]
        [JsonProperty("region")]
        public string Region { get; set; }

        [StringLength(50)]
        [JsonProperty("city")]
        public string City { get; set; }

        [StringLength(50)]
        [JsonProperty("county")]
        public string County { get; set; }

        [StringLength(50)]
        [JsonProperty("isp")]
        public string Isp { get; set; }

        [StringLength(50)]
        [JsonProperty("country_id")]
        public string Country_id { get; set; }

        [JsonProperty("area_id")]
        public string Area_id { get; set; }

        [JsonProperty("region_id")]
        public string Region_id { get; set; }

        [JsonProperty("city_id")]
        public string City_id { get; set; }

        [StringLength(50)]
        [JsonProperty("county_id")]
        public string County_id { get; set; }

        [JsonProperty("isp_id")]
        public string Isp_id { get; set; }
    }
}
