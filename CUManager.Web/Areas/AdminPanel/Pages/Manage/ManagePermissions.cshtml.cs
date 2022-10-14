using CUManager.Data.Helpers;
using CUManager.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUManager.Web.Areas.AdminPanel.Pages.Manage
{
    public class ManagePermissionsModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManagePermissionsModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public PermissionViewModel PermissionModel { get; set; }
        [BindProperty]
        public List<RoleClaimsViewModel> AllPermissions { get; set; }

        public async Task<IActionResult> OnGetAsync(string roleId)
        {
            PermissionModel = new PermissionViewModel();
            AllPermissions = new List<RoleClaimsViewModel>();
            AllPermissions.GetPermissions(typeof(Permissions.AllPermissions), roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            PermissionModel.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = AllPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in AllPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
                else
                {
                    permission.Selected = false;
                }
            }
            PermissionModel.RoleClaims = AllPermissions;
            return Page();
        }

        public async Task<IActionResult> OnPostUpdatePermissions(PermissionViewModel permissionModel)
        {
            var role = await _roleManager.FindByIdAsync(permissionModel.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = permissionModel.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            var result = new
            {
                IsDone = true
            };
            return new JsonResult(result);
        }
    }
}
