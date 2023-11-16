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
        private Workspace _workSpace;

        public CurrencyType Currency { get; set; }

        public Workspace WorkSpace
        {
            get
            {
                return this._workSpace;
            }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("No se admite un workspace vacio");
                }
                this._workSpace = value;
            }
        }

        protected double GetExchangeValueOfDay(DateTime date, CurrencyType currency)
        {
            Exchange exchange = this.WorkSpace.Exchanges.MaxBy(x => x.Date <= date && x.Currency == currency );
            return exchange.CurrencyValue;
        }
    }

}
