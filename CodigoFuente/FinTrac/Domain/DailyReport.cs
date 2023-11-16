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
            List<Account> accounts = this.WorkSpace.Accounts;
            foreach (Account account in accounts)
            {
                List<Transactions> transactions = account.Transactions;
                foreach (Transactions transaction in transactions)
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
                                double currencyValue = this.GetExchangeValueOfDay(transaction.CreationDate, transaction.Currency);
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
                                double currencyValue = this.GetExchangeValueOfDay(transaction.CreationDate, transaction.Currency);
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
