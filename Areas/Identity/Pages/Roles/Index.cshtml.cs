using IdentityDemo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityDemo.Areas.Identity.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        [BindProperty]
        public IList<IdentityRole> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Roles =  await _roleManager.Roles.ToListAsync();
            return Page();
        }
    }
}
