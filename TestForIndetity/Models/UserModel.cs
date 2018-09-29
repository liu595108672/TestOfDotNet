using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForIndetity.Models
{
    public class UserTable
    {
        public int ID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public int RoleId { set; get; }
    }
}
