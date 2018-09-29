using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForWebAPI.Models
{
    public class AppOptions
    {
        public string Test { set; get; }
        public string Test2 { set; get; }
        public List<PersonOption> TestList { set; get; }
    }

    public class AppOptions2
    {
        public string Test { set; get; }
    }
    public class PersonOption
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
