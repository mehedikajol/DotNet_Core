using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdentityDemo.Data;
using IdentityDemo.Entities;
using Microsoft.AspNetCore.Authorization;

namespace IdentityDemo.Pages.Products
{
    [Authorize(Policy = "temp")]
    public class DetailsModel : PageModel
    {
        private readonly IdentityDemo.Data.AppDbContext _context;

        public DetailsModel(IdentityDemo.Data.AppDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                .Include(p => p.User).FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
