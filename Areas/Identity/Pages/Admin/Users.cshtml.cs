using IdentityDemo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Areas.Identity.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly AppDbContext _context;
        public UsersModel(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IdentityUser> Users { get; set; } = Enumerable.Empty<IdentityUser>();

        public async Task OnGet()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
