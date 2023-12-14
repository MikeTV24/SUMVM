using Microsoft.AspNetCore.Identity;

namespace SUMVM.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}   