using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CUManager.Web.Areas.AdminPanel.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        public CreateModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public NewUserModel NewUser { get; set; } = new NewUserModel();

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = new IdentityUser
            {
                Email = NewUser.Email,
                UserName = NewUser.Email
            };
            var result = await _userManager.CreateAsync(user, NewUser.Password);
            return RedirectToPage("./Index");
        }

        public class NewUserModel
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "Repeat Password should match")]
            public string RepeatPassword { get; set; }
        }
    }
}
