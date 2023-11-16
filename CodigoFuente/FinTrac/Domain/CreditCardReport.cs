using Domain.DataTypes;

namespace Domain
{
    public class CreditCardReport : Report
    {
        public List<Transaction> CalculateCreditCardReport(CreditCard creditCard)
        {
            List<Transaction> transactionList = new List<Transaction>();
            foreach (Account account in WorkSpace.Accounts)
            {
                transactionList.AddRange(account.Transactions.Where(x => x.Category.Type == CategoryType.Cost && x.CreationDate.Day > creditCard.DeadLine && x.CreationDate.Month == DateTime.Today.Month - 1));
                transactionList.AddRange(account.Transactions.Where(x => x.Category.Type == CategoryType.Cost && x.CreationDate.Day <= creditCard.DeadLine && x.CreationDate.Month == DateTime.Today.Month));
            }
            return transactionList;
        }
    }
}
