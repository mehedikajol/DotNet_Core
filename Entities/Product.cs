using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityDemo.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
