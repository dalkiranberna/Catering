using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Extensions
{
    public static class UserExtensions
    {
        public static string GetTC(this IIdentity id) //iprincipal
        {
            var allId = (ClaimsIdentity)id;
            return allId.FindFirst("TC").Value;
        }

        public static string GetNameSurname(this IIdentity id)
        {
            var allId = (ClaimsIdentity)id;
            return allId.FindFirst("NameLastname").Value;
        }
    }
}
