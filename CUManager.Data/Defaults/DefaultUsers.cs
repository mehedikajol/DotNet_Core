using CUManager.Constants;
using CUManager.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static CUManager.Data.Helpers.Permissions;

namespace CUManager.Data.Defaults
{
    public static class DefaultUsers
    {
        /*
        public static async Task SeedBasicUserAsync(
           UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "basic@ums.com",
                Email = "basic@ums.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "P@ssw0rd");
                    await userManager.AddToRoleAsync(defaultUser, InitialRole.Basic.ToString());
                }
            }
        }
        */

        public static async Task SeedSuperAdminAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "super@ums.com",
                Email = "super@ums.com",
                EmailConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "P@ssw0rd");
                    await userManager.AddToRoleAsync(defaultUser, InitialRole.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");

            /*
            var moduleList = new List<string>();
            foreach(var module in moduleList)
            {
                await roleManager.AddPermissionClaim(adminRole, module);
            }
            */
            var selectedClaims = AllPermissions.GetPermissionList();
            foreach (var claim in selectedClaims)
            {
                await roleManager.AddPermissionClaim(adminRole, claim);
            }
            //await roleManager.AddPermissionClaims(adminRole, "Posts");
        }

        public static async Task AddPermissionClaims(
            this RoleManager<IdentityRole> roleManager,
            IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
