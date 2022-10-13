using IdentityDemo.Data;
using IdentityDemo.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDemo.Areas.Identity.Pages.Roles
{
    public class ManageClaimsModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public ManageClaimsModel(RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public List<ClaimViewModel> AllClaims { get; set; } = new List<ClaimViewModel>();
        public string RoleId { get; set; }
        //public IEnumerable<Claim> Claims { get; set; }

        public  async Task<IActionResult> OnGetAsync(string Id)
        {
            RoleId = Id;

            var role = await _roleManager.FindByIdAsync(Id);
            var Claims = await _roleManager.GetClaimsAsync(role);


            foreach (var claim in Constants.ClaimsList.AllClaim)
            {
                var index = AllClaims.Count;
                var newClaim = new ClaimViewModel
                {
                    Claim = claim.Value,
                    IsSelected = false
                };

                foreach (var existingClaim in Claims)
                {
                    if(existingClaim.Value == claim.Value)
                    {
                        newClaim = new ClaimViewModel
                        {
                            Claim = claim.Value,
                            IsSelected = true
                        };
                    }
                }
                AllClaims.Add(newClaim);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAssignClaimsAsync(string roleId, List<ClaimViewModel> allClaims)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            var Claims = await _roleManager.GetClaimsAsync(role);

            foreach(var claim in AllClaims)
            {
                Claim newClaim = new Claim(claim.Claim, claim.Claim);
                if(claim.IsSelected == true)
                {
                    await _roleManager.AddClaimAsync(role, newClaim);
                }
                else
                {
                    await _roleManager.RemoveClaimAsync(role, newClaim);
                }
            }
            var returnMessage = new
            {
                IsDone = true,
                Message = "Can't remove from existing roles!"
            };
            return new JsonResult(returnMessage);
        }
    }
}
