using Microsoft.AspNetCore.Identity;

namespace Tsakaty.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
