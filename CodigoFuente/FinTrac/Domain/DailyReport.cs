using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataTypes;

namespace Domain
{
    public class DailyReport : Report
    {
        public DateTime Date { get; set; }
        public Month Month { get; set; }
        public double Income { get; set; }
        public double Expenses { get; set; }
        public void CalculateReport()
        {
            List<Account> accounts = this.WorkSpace.AccountList;
            foreach (Account account in accounts)
            {
                List<Transaction> transactions = account.TransactionList;
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.CreationDate == Date)
                    {
                        if (transaction.Category.Type == CategoryType.Cost)
                        {
                            if (account.Currency == CurrencyType.UYU)
                            {
                                Expenses += transaction.Amount;
                            }
                            else
                            {
                                double currencyValue = this.GetExchangeValueOfDay(transaction.CreationDate);
                                double transactionAmountInUYU = transaction.Amount * currencyValue;
                                Expenses += transactionAmountInUYU;
                            }
                        }
                        else
                        {
                            if (account.Currency == CurrencyType.UYU)
                            {
                                Income += transaction.Amount;
                            }
                            else
                            {
                                double currencyValue = this.GetExchangeValueOfDay(transaction.CreationDate);
                                double transactionAmountInUYU = transaction.Amount * currencyValue;
                                Income += transactionAmountInUYU;
                            }
                        }
                    }
                }
            }
        }
    }
}
