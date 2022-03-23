using System;
using System.Collections.Generic;

namespace My2Cents.DataInfrastructure
{
    public partial class StockAsset
    {
        public int StockAssetId { get; set; }
        public int StockId { get; set; }
        public int UserId { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public decimal Quantity { get; set; }

        public virtual Stock Stock { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
