using CUManager.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUManager.Web.Areas.AdminPanel.Pages.Manage
{
    public class ManageUserRoleModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageUserRoleModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public ManageUserRolesViewModel ManageUserRolesViewModel { get; set; }

        public string UserID { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            ManageUserRolesViewModel = new ManageUserRolesViewModel()
            {
                UserId = userId,
                UserRoles = viewModel
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAssignRoles(string id, ManageUserRolesViewModel manageUserRolesViewModel)
        {
            var user = await _userManager.FindByIdAsync(id);
            var returnMessage = new
            {
                IsDone = true,
                Message = "Roles are assigned to user!"
            };
            if (user == null)
            {
                return NotFound();
            }
            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                returnMessage = new
                {
                    IsDone = false,
                    Message = "Can't remove from existing roles!"
                };
                return new JsonResult(returnMessage);
            }
            result = await _userManager.AddToRolesAsync(user, manageUserRolesViewModel.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                returnMessage = new
                {
                    IsDone = false,
                    Message = "Can't add selected roles to user!"
                };
                return new JsonResult(returnMessage);
            }
            return new JsonResult(returnMessage);
        }
    }
}
