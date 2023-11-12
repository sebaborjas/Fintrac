using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GoalsReport : Report
    {
        private double _amountSpent;

        public bool GoalAchieved { get; set; }

        public double AmountSpent
        {
            get
            {
                return _amountSpent;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El monto definido no puede ser negativo");
                }
                _amountSpent = value;
            }
        }

        public Goal Goal { get; set; }


        public void CalculateReport()
        {
            List<Account> accounts = this.WorkSpace.AccountList;
            foreach (Category category in Goal.Categories)
            {
                foreach (Account account in accounts)
                {
                    List<Transaction> transactions = account.TransactionList;
                    foreach (Transaction transaction in transactions)
                    {
                        if (transaction.Category == category && transaction.CreationDate.Month == DateTime.Today.Month)
                        {
                            if (account.Currency == DataTypes.CurrencyType.UYU)
                            {
                                AmountSpent += transaction.Amount;
                            }
                            else
                            {
                                double currencyValue = this.GetExchangeValueOfDay(transaction.CreationDate);
                                double transactionAmountInUYU = transaction.Amount * currencyValue;
                                AmountSpent += transactionAmountInUYU;
                            }
                        }
                    }
                }
            }
            GoalAchieved = AmountSpent <= Goal.MaxAmount;
        }
    }
}