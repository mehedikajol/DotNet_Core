using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDemo.Areas.Identity.Pages.Admin
{
    public class ClaimsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ClaimsModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public IEnumerable<Claim> Claims { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                return RedirectToPage("/");
            }

            var user = await _userManager.FindByIdAsync(Id);
            Claims = await _userManager.GetClaimsAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAddClaimAsync(
            [Required] string type,
            [Required] string value)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (ModelState.IsValid)
            {
                var claim = new Claim(type, value);
                var result = await _userManager.AddClaimAsync(user, claim);
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
            }
            Claims = await _userManager.GetClaimsAsync(user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditClaimAsync(
            [Required] string type,
            [Required] string value,
            [Required] string oldValue)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var newClaim = new Claim(type, value);
                var oldClaim = new Claim(type, oldValue);
                var result = await _userManager.ReplaceClaimAsync(user, oldClaim, newClaim);
            }
            Claims = await _userManager.GetClaimsAsync(user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteClaimAsync(
            [Required] string type,
            [Required] string value)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var claim = new Claim(type, value);
                var result = await _userManager.RemoveClaimAsync(user, claim);
            }
            Claims = await _userManager.GetClaimsAsync(user);
            return RedirectToPage();
        }
    }
}
