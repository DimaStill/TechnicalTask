using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TechnicalTask.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "resourceType")]
        public string ResourceType { get; set; }
        [JsonProperty(PropertyName = "name")]
        public List<HumanName> Name { get; set; }
        [JsonProperty(PropertyName = "birthDate")]
        public DateTime BirthDate { get; set; }
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }
    }
}
