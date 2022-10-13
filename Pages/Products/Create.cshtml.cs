using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IdentityDemo.Data;
using IdentityDemo.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IdentityDemo.Pages.Products
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IdentityDemo.Data.AppDbContext _context;

        public CreateModel(IdentityDemo.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.UserId = await _context.Users
                .Where(u => u.UserName == HttpContext.User.Identity.Name)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
