using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalTask.Models
{
    public class Person
    {
        public string ResourceType { get; set; }
        public List<HumanName> Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Acive { get; set; }
    }
}
