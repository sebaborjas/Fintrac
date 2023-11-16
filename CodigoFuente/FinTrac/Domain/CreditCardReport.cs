using Domain.DataTypes;

namespace Domain
{
    public class CreditCardReport : Report
    {
        public List<Transactions> CalculateCreditCardReport(CreditCard creditCard)
        {
            List<Transactions> transactionList = new List<Transactions>();
            foreach (Account account in WorkSpace.Accounts)
            {
                transactionList.AddRange(account.Transactions.Where(x => x.Category.Type == CategoryType.Cost && x.CreationDate.Day > creditCard.DeadLine && x.CreationDate.Month == DateTime.Today.Month - 1));
                transactionList.AddRange(account.Transactions.Where(x => x.Category.Type == CategoryType.Cost && x.CreationDate.Day <= creditCard.DeadLine && x.CreationDate.Month == DateTime.Today.Month));
            }
            return transactionList;
        }
    }
}
