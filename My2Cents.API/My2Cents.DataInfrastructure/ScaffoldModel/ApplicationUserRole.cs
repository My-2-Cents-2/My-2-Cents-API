using Microsoft.AspNetCore.Identity;

namespace My2Cents.DataInfrastructure
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}