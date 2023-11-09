using Domain.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ExchangeQueryParameters
    {
        public Workspace Workspace { get; set; }
        public DateTime Date { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}
