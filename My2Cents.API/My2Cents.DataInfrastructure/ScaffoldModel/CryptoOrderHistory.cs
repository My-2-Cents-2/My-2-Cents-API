using System;
using System.Collections.Generic;

namespace My2Cents.DataInfrastructure
{
    public partial class CryptoOrderHistory
    {
        public int CryptoOrderId { get; set; }
        public int UserId { get; set; }
        public int CryptoId { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal Quantity { get; set; }
        public string OrderType { get; set; } = null!;
        public DateTime OrderTime { get; set; }

        public virtual Crypto Crypto { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
