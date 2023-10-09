using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataTypes;

namespace Domain
{
    public abstract class Report
    {
        public CurrencyType Currency { get; set; }
        public Month Month { get; set; }
        public Workspace WorkSpace { get; set; }
    }
}
