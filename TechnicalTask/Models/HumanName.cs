using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalTask.Models
{
    public class HumanName
    {
        public string Text { get; set; }
        public string Family { get; set; }
        public List<string> Given { get; set; }
        public List<string> Prefix { get; set; }
        public List<string> Suffix { get; set; }
    }
}
