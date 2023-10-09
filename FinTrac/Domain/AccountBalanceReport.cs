using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccountBalanceReport : Report
    {
        public PersonalAccount Account { get; set; }

        public double CalculateBalance()
        {
            return 1;
        }
    }
}
