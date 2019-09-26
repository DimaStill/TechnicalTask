using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TechnicalTask.Models
{
    public class HumanName
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "family")]
        public string Family { get; set; }
        [JsonProperty(PropertyName = "given")]
        public List<string> Given { get; set; }
        [JsonProperty(PropertyName = "prefix")]
        public List<string> Prefix { get; set; }
        [JsonProperty(PropertyName = "suffix")]
        public List<string> Suffix { get; set; }
    }
}
