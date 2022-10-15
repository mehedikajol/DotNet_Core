using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CUManager.Web.Areas.AdminPanel.Pages.Manage
{
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IList<IdentityRole> Roles { get; set; }

        [BindProperty]
        public string NewRole { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            NewRole = "";
            Roles = await _roleManager.Roles.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (NewRole != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(NewRole.Trim()));
            }
            return RedirectToPage("./Roles");
        }
    }
}
