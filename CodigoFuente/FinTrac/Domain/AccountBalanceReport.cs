using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Domain.DataTypes;

namespace Domain
{
    public class AccountBalanceReport : Report
    {
        public int Id { get; set; }

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
            double ingresos = 0;
            double costos = 0;

            double balance = 0;

            foreach (var transaction in Account.TransactionList)
            {
                if(transaction.Currency == CurrencyType.UYU)
                {
                    if (transaction.Category.Type == CategoryType.Income)
                    {
                        ingresos += transaction.Amount;
                    }
                    else
                    {
                        costos += transaction.Amount;
                    }

                    balance = this.Account.StartingAmount + ingresos - costos;
                }
                else
                {
                    if (transaction.Category.Type == CategoryType.Income)
                    {
                        ingresos += transaction.Amount * this.GetCurrencyVallueOfDay(transaction.CreationDate);
                    }
                    else
                    {
                        costos += transaction.Amount * this.GetCurrencyVallueOfDay(transaction.CreationDate);
                    }
                    balance = this.Account.StartingAmount * this.GetCurrencyVallueOfDay(Account.CreationDate) + ingresos - costos;

                }
                
            }
            return balance;
        }

        private Double GetCurrencyVallueOfDay(DateTime date)
        {
        
            Exchange exchange = Account.WorkSpace.ExchangeList.MaxBy(x => x.Date <= date && x.Currency == Currency);

            return exchange.CurrencyValue;
        }
    }
}
