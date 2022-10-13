using IdentityDemo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityDemo.Areas.Identity.Pages.Roles
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateModel(AppDbContext context,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        [BindProperty]
        public IdentityRole Role { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            

            var result = await _roleManager.CreateAsync(Role);

            var claims = await _roleManager.GetClaimsAsync(Role);

            return RedirectToPage("./Index");
        }
    }
}
