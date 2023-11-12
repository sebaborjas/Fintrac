using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataTypes;

namespace Domain
{
	public class CategoryReport : Report
	{
		public Category Category { get; set; }

		public Month Month { get; set; }

		public double AmountSpent { get; set; } = 0;

		public string PercentageAboutTotal { get; set; } = "0";

		public string CalculateReport() 
		{
			string categoryName = this.Category.Name;
			double totalSpent = 0;
			double categorySpent = 0;

			List<Account> accounts = this.WorkSpace.AccountList;
			foreach (Account account in accounts)
			{
				List<Transaction> transactions = account.TransactionList;
				foreach (Transaction transaction in transactions)
				{
					if((Month)transaction.CreationDate.Month == this.Month) 
					{
						if (transaction.Currency == CurrencyType.UYU)
						{
							totalSpent += transaction.Amount;
							if (transaction.Category == this.Category)
							{
								categorySpent += transaction.Amount;
							}

						}
						else
						{
							double currencyValue = this.GetExchangeValueOfDay(transaction.CreationDate);
							double transactionAmountInUYU = transaction.Amount * currencyValue;
							totalSpent += transactionAmountInUYU;
							if (transaction.Category == this.Category)
							{
								categorySpent += transactionAmountInUYU;
							}
						}
					}

					
                    
                }
			}
			double percentage = 0;

			if (totalSpent != 0)
			{
				percentage = Math.Round(categorySpent * 100 / totalSpent);
			}

			string reportFormatOutput = $"{categoryName}: {categorySpent} => {percentage} %";

			return reportFormatOutput;
		}

	}
}
