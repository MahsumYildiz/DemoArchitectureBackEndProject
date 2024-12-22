using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimsExtensions
    {
        public static void AddName(this ICollection<Claim>Claims,string name)
        {
            Claims.Add(new Claim(ClaimTypes.Name,name));
        }
        public static void AddRoles(this ICollection<Claim> Claims, string[] roles)
        {
            roles.ToList().ForEach(roles => Claims.Add(new Claim(ClaimTypes.Role, roles)));
        }
    }
}
