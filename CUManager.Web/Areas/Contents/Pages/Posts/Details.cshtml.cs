using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CUManager.Data.Contexts;
using CUManager.Data.Entities;

namespace CUManager.Web.Areas.Contents.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly CUManager.Data.Contexts.AppDbContext _context;

        public DetailsModel(CUManager.Data.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Posts.FirstOrDefaultAsync(m => m.PostId == id);

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
