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
		public List<Transactions> Expenses()
		{
			List<Transactions> transactions = new List<Transactions>();
			foreach (Account account in WorkSpace.Accounts)
			{
				transactions.AddRange(account.Transactions.Where(x => x.Category.Type == CategoryType.Cost));
			}
			return transactions;
		}

		public List<Transactions> ExpensesByCategory(string categoryName)
		{
			List<Transactions> transactions = new List<Transactions>();
			foreach (Account account in WorkSpace.Accounts)
			{
				transactions.AddRange(account.Transactions.Where(x => x.Category.Name == categoryName));
			}
			return transactions;
		}

		public List<Transactions> ExpensesByDate(DateTime fromDate, DateTime toDate)
		{
			if (fromDate > toDate)
			{
				throw new InvalidDateException();
			}
			List<Transactions> transactions = new List<Transactions>();
			foreach (Account account in WorkSpace.Accounts)
			{
				transactions.AddRange(account.Transactions.Where(x => x.CreationDate >= fromDate && x.CreationDate <= toDate));
			}
			return transactions;
		}

		public List<Transactions> ExpensesByAccount(string accountName)
		{
			List<Transactions> transactions = new List<Transactions>();
			foreach (Account account in WorkSpace.Accounts)
			{
				transactions.AddRange(account.Transactions.Where(x => x.Account.Name == accountName));
			}
			return transactions;
		}
	}
}
