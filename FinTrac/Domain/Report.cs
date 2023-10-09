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
        private CurrencyType currency;
        private Month month;
        private Object workSpace;
    }
}
