using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataTypes;
using Domain.Exceptions;

namespace Domain
{
    public class ExpensesReport : Report
    {
        public List<Transaction> ListExpenses()
        {

            List<Transaction> transactionList = new List<Transaction>();
            foreach (Account account in WorkSpace.AccountList)
            {
                transactionList.AddRange(account.TransactionList.Where(x => x.Category.Type == CategoryType.Cost));
            }
            return transactionList;

        }

        public List<Transaction> ListExpensesByCategory(string categoryName)
        {
            List<Transaction> transactionList = new List<Transaction>();
            foreach (Account account in WorkSpace.AccountList)
            {
                transactionList.AddRange(account.TransactionList.Where(x => x.Category.Name == categoryName));
            }

            return transactionList;
        }

        public List<Transaction> ListExpensesByDate(DateTime fromDate, DateTime toDate)
        {
            if(fromDate > toDate)
            {
                throw new InvalidDateException();
            }
            List<Transaction> transactionList = new List<Transaction>();
            foreach (Account account in WorkSpace.AccountList)
            {
                transactionList.AddRange(account.TransactionList.Where(x => x.CreationDate >= fromDate && x.CreationDate <= toDate));
            }

            return transactionList;
        }

        public List<Transaction> ListExpensesByAccount(string accountName)
        {
            List<Transaction> transactionList = new List<Transaction>();
            foreach (Account account in WorkSpace.AccountList)
            {
                transactionList.AddRange(account.TransactionList.Where(x => x.Account.Name == accountName));
            }

            return transactionList;
        }
    }
}
