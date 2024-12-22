using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List <String> Claims(this ClaimsPrincipal principal,string claimType)
        {
            var result = principal?.FindAll(claimType)?.Select(p=>p.Value).ToList();
            return result;
        }
        public static List<String> ClaimsRole(this ClaimsPrincipal principal)
        {
            return principal?.Claims(ClaimTypes.Role);
            
        }
    }
}
