using System;
using System.Collections.Generic;

namespace My2Cents.DataInfrastructure
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            CryptoAssets = new HashSet<CryptoAsset>();
            CryptoOrderHistories = new HashSet<CryptoOrderHistory>();
            StockAssets = new HashSet<StockAsset>();
            StockOrderHistories = new HashSet<StockOrderHistory>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string EmailVerified { get; set; } = null!;

        public virtual UserProfile UserProfile { get; set; } = null!;
        public virtual ICollection<CryptoAsset> CryptoAssets { get; set; }
        public virtual ICollection<CryptoOrderHistory> CryptoOrderHistories { get; set; }
        public virtual ICollection<StockAsset> StockAssets { get; set; }
        public virtual ICollection<StockOrderHistory> StockOrderHistories { get; set; }
    }
}
