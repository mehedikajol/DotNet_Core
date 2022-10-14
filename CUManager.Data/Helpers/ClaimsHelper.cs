using CUManager.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CUManager.Data.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
        {
            //allPermissions = new List<RoleClaimsViewModel>();

            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fi in fields)
            {
                var roleClaimsViewModel = new RoleClaimsViewModel
                {
                    Value = fi.GetValue(null).ToString(),
                    Type = "Permissions",
                    Selected = true
                };

                allPermissions.Add(roleClaimsViewModel);
            }
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
