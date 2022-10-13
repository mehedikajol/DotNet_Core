using IdentityDemo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDemo.Areas.Identity.Pages.Roles
{
    public class ManageClaimsModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public ManageClaimsModel(RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        
        public IEnumerable<Claim> Claims { get; set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            Claims = await _roleManager.GetClaimsAsync(role);
            return Page();
        }
    }
}
