using Mem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mem.Services
{
    public class UserService
    {
        internal bool ValidatePassword(string username, string password)
        {
            return true;
        }

        internal UserModel GetUser(string username)
        {
            return new UserModel
            {
                ID = 1,
                Name = "Foo",
                Username = "FooUserName"
            };
        }

        internal UserModel GetUser(int ID)
        {
            return new UserModel
            {
                ID = 1,
                Name = "Foo",
                Username = "FooUserName"
            };
        }
    }
}
