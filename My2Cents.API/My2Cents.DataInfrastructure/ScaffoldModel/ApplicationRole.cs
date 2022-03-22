using Microsoft.AspNetCore.Identity;

namespace My2Cents.DataInfrastructure
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}