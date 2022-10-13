using System.Collections.Generic;

namespace IdentityDemo.Data
{
    public class ApplicationClaimTypes
    {
        public static List<string> ApplicationClaims = new List<string>()
        {
            "Admin",
            "Employee",
            "DoB",
            "User",
            "VIP",
            "Disabled"
        };
    }
}
