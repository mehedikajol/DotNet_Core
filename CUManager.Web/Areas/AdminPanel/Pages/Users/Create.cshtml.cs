using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

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

            if (result.Succeeded)
            {
                var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var code = HttpUtility.UrlEncode(confirmToken);
                var confirmMailLink = Url.PageLink(pageName: "/Account/ConfirmEmail", values: new { area = "Identity", userId = user.Id, code = confirmToken });
                //return LocalRedirect("/Identity/Account/ConfirmEmail", new { userId = user.Id, token = confirmToken });

                var message = new MailMessage("mail@gmail.com", user.Email, "Please confirm your account", $"Please clink on this link to confirm your account : {confirmMailLink}");

                using(var emailClinet = new SmtpClient("smtp.gmail.com", 587))
                {
                    emailClinet.Credentials = new NetworkCredential(
                        "mail@gmail.com",
                        "password"
                        );
                    emailClinet.EnableSsl = true;
                    await emailClinet.SendMailAsync(message);
                }
                return RedirectToPage("./Index");
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("Register", err.Description);
                }
                return Page();
            }

        }

        public class NewUserModel
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /*
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "Repeat Password should match")]
            public string RepeatPassword { get; set; }
            */
        }
    }
}
