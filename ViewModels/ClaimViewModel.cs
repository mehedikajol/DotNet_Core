using System.Security.Claims;

namespace IdentityDemo.ViewModels
{
    public class ClaimViewModel
    {
        public Claim Claim { get; set; }
        public bool IsSelected { get; set; }
    }
}
