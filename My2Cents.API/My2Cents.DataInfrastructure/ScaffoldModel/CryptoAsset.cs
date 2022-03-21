using System;
using System.Collections.Generic;

namespace My2Cents.DataInfrastructure
{
    public partial class CryptoAsset
    {
        public int CryptoAssetId { get; set; }
        public int CryptoId { get; set; }
        public int UserId { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public decimal Quantity { get; set; }

        public virtual Crypto Crypto { get; set; } = null!;
        public virtual UserLogin User { get; set; } = null!;
    }
}
