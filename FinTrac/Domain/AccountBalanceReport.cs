using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccountBalanceReport : Report
    {
        private PersonalAccount _account;
        public PersonalAccount Account 
        {
            get 
            { 
                return this._account; 
            }
            set 
            {
                if (value == null)
                {
                    throw new NullReferenceException("No se admite una cuenta vacia"); 
                }
                this._account = value;
            } 
        }

        public double CalculateBalance()
        {
            return 200;
        }
    }
}
