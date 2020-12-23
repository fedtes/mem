using Mem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mem.Services
{
    public class UserService
    {

        private UserModel[] mock = { new UserModel() { ID = 1, Salt = "ukSeY89FCXJLIZa9wOIhpA==", Name = "Demo User", Username = "testuser", Hash = "ebYewHArxcnDpdB6/JnvKI2oGVrNED0hkD2Ey8zpqdx/AqhaSpa0bikMCFr3i6Mbdk+khLSwCi2l64CuO/60JRZTEpvxdn3QFLBUy2VPdIbGhk+J814AqIsAuK/faTGiLT4GSHCIJmHpyDi3dsIn9RT7IXHfzetZLSqtHWy84o+jDlDXDmFdtSpkFpECSzdyk+EtVld1wg+2K9uQaI7PUwWMDuOqwpVX/ayzfVSni+36AZUwE0ws1dQ4JAreRpqQdzqrPWigvH6ruz9z1xdhmGyhZKmzIbPA2ODKPy7zTsBWJl6alOgpBbHKlGBaq2rsOp/if/s4R2zMVmKxD3CKoQ==" } };

        private readonly SecurityService securityService;

        public UserService(SecurityService securityService)
        {
            this.securityService = securityService;
        }

        internal bool ValidatePassword(string username, string password)
        {
            var u = GetUser(username);
            if (u is null) return false;
            var hash = securityService.Hash(password, u.Salt);
            return hash == u.Hash;
        }

        internal UserModel GetUser(string username)
        {
            return mock.FirstOrDefault(x => x.Username.ToLowerInvariant() == username.ToLowerInvariant());
        }

        internal UserModel GetUser(int ID)
        {
            return mock.FirstOrDefault(x => x.ID == ID);
        }
    }
}
