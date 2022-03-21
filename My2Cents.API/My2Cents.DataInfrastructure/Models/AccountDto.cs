using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2Cents.DataInfrastructure.Models
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal TotalBalance { get; set; }
        public int AccountTypeId { get; set; }
        public decimal Interest { get; set; }

    }
}
