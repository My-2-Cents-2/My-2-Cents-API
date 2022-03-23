using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace My2Cents.DataInfrastructure
{
    public partial class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            CryptoAssets = new HashSet<CryptoAsset>();
            CryptoOrderHistories = new HashSet<CryptoOrderHistory>();
            StockAssets = new HashSet<StockAsset>();
            StockOrderHistories = new HashSet<StockOrderHistory>();
        }

        public virtual UserProfile UserProfile { get; set; } = null!;
        public virtual ICollection<CryptoAsset> CryptoAssets { get; set; }
        public virtual ICollection<CryptoOrderHistory> CryptoOrderHistories { get; set; }
        public virtual ICollection<StockAsset> StockAssets { get; set; }
        public virtual ICollection<StockOrderHistory> StockOrderHistories { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
