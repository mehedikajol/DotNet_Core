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
    public class IndexModel : PageModel
    {
        private readonly CUManager.Data.Contexts.AppDbContext _context;

        public IndexModel(CUManager.Data.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; }

        public async Task OnGetAsync()
        {
            Post = await _context.Posts.ToListAsync();
        }
    }
}
