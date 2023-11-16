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
		public List<Transaction> Expenses()
		{
			List<Transaction> transactions = new List<Transaction>();
			foreach (Account account in WorkSpace.Accounts)
			{
				transactions.AddRange(account.Transactions.Where(x => x.Category.Type == CategoryType.Cost));
			}
			return transactions;
		}

		public List<Transaction> ExpensesByCategory(string categoryName)
		{
			List<Transaction> transactions = new List<Transaction>();
			foreach (Account account in WorkSpace.Accounts)
			{
				transactions.AddRange(account.Transactions.Where(x => x.Category.Name == categoryName));
			}
			return transactions;
		}

		public List<Transaction> ExpensesByDate(DateTime fromDate, DateTime toDate)
		{
			if (fromDate > toDate)
			{
				throw new InvalidDateException();
			}
			List<Transaction> transactions = new List<Transaction>();
			foreach (Account account in WorkSpace.Accounts)
			{
				transactions.AddRange(account.Transactions.Where(x => x.CreationDate >= fromDate && x.CreationDate <= toDate));
			}
			return transactions;
		}

		public List<Transaction> ExpensesByAccount(string accountName)
		{
			List<Transaction> transactions = new List<Transaction>();
			foreach (Account account in WorkSpace.Accounts)
			{
				transactions.AddRange(account.Transactions.Where(x => x.Account.Name == accountName));
			}
			return transactions;
		}
	}
}
