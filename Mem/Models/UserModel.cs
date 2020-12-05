using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mem.Models
{
    public class UserModel : BaseModel
    {
        public String Name;
        public String Username;
    }

    public class LoginModel
    {
        public String username { get; set; }
        public String password { get; set; }
    }
}
