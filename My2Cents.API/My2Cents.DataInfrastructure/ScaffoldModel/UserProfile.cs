using System;
using System.Collections.Generic;

namespace My2Cents.DataInfrastructure
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Accounts = new HashSet<Account>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? SecondaryEmail { get; set; }
        public string MailingAddress { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Employer { get; set; } = null!;
        public string WorkAddress { get; set; } = null!;
        public string WorkPhone { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
