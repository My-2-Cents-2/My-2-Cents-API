using System;
using System.Collections.Generic;

namespace My2Cents.DataInfrastructure
{
    public partial class Crypto
    {
        public Crypto()
        {
            CryptoAssets = new HashSet<CryptoAsset>();
            CryptoOrderHistories = new HashSet<CryptoOrderHistory>();
        }

        public int CryptoId { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Name { get; set; } = null!;
        public string ShortenedName { get; set; } = null!;

        public virtual ICollection<CryptoAsset> CryptoAssets { get; set; }
        public virtual ICollection<CryptoOrderHistory> CryptoOrderHistories { get; set; }
    }
}
