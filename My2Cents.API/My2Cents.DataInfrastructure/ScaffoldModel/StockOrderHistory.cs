using System;
using System.Collections.Generic;

namespace My2Cents.DataInfrastructure
{
    public partial class StockOrderHistory
    {
        public int StockOrderId { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal Quantity { get; set; }
        public string OrderType { get; set; } = null!;
        public DateTime OrderTime { get; set; }

        public virtual Stock Stock { get; set; } = null!;
        public virtual UserLogin User { get; set; } = null!;
    }
}
