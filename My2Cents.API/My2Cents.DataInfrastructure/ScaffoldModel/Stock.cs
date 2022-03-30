using System;
using System.Collections.Generic;

namespace My2Cents.DataInfrastructure
{
    public partial class Stock
    {
        public Stock()
        {
            StockAssets = new HashSet<StockAsset>();
            StockOrderHistories = new HashSet<StockOrderHistory>();
        }

        public int StockId { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime LastUpdate { get; set; }
        public decimal PriceChange { get; set; }
        public double PriceChangePercentage { get; set; }
        public string Name { get; set; } = null!;
        public string ShortenedName { get; set; } = null!;

        public virtual ICollection<StockAsset> StockAssets { get; set; }
        public virtual ICollection<StockOrderHistory> StockOrderHistories { get; set; }
    }
}
