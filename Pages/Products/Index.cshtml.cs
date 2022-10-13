using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdentityDemo.Data;
using IdentityDemo.Entities;

namespace IdentityDemo.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IdentityDemo.Data.AppDbContext _context;

        public IndexModel(IdentityDemo.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products
                .Include(p => p.User).ToListAsync();
        }
    }
}
