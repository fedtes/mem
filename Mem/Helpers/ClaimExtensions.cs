using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mem.Helpers
{
    public static class ClaimExtensions
    {
        public static string NameIdentifier(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
