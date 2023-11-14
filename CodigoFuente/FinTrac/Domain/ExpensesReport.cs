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
        public List<Transactions> ListExpenses()
        {

            List<Transactions> transactionList = new List<Transactions>();
            foreach (Account account in WorkSpace.AccountList)
            {
                transactionList.AddRange(account.TransactionList.Where(x => x.Category.Type == CategoryType.Cost));
            }
            return transactionList;

        }

        public List<Transactions> ListExpensesByCategory(string categoryName)
        {
            List<Transactions> transactionList = new List<Transactions>();
            foreach (Account account in WorkSpace.AccountList)
            {
                transactionList.AddRange(account.TransactionList.Where(x => x.Category.Name == categoryName));
            }

            return transactionList;
        }

        public List<Transactions> ListExpensesByDate(DateTime fromDate, DateTime toDate)
        {
            if(fromDate > toDate)
            {
                throw new InvalidDateException();
            }
            List<Transactions> transactionList = new List<Transactions>();
            foreach (Account account in WorkSpace.AccountList)
            {
                transactionList.AddRange(account.TransactionList.Where(x => x.CreationDate >= fromDate && x.CreationDate <= toDate));
            }

            return transactionList;
        }

        public List<Transactions> ListExpensesByAccount(string accountName)
        {
            List<Transactions> transactionList = new List<Transactions>();
            foreach (Account account in WorkSpace.AccountList)
            {
                transactionList.AddRange(account.TransactionList.Where(x => x.Account.Name == accountName));
            }

            return transactionList;
        }
    }
}
