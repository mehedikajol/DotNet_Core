using IdentityDemo.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityDemo.Areas.Identity.Pages.Claims
{
    public class IndexModel : PageModel
    {
        public Dictionary<string,string> AllClaim { get; set; }

        public void OnGet()
        {
            AllClaim = ClaimsList.AllClaim;
        }
    }
}
